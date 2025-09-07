using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Doc.Sample;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Presentation.Dto.Localizacao;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Dto.Output;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;

namespace Motos.Presentation.Controllers
{
    [Route("api/v1/motos")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;
        private readonly ILocalizacaoService _localizacaoService;
        private readonly IMapper _mapper;

        public MotoController(IMotoService motoService, ILocalizacaoService localizacaoService, IMapper mapper)
        {
            _motoService = motoService;
            _localizacaoService = localizacaoService;
            _mapper = mapper;
        }


        [SwaggerOperation(
            Summary = "Obtém todas as motos de um pátio específico",
            Description = "Retorna uma lista de motos associadas ao ID do pátio fornecido"
        )]
        [SwaggerResponse(200, "Lista de motos retornada com sucesso", typeof(IEnumerable<ObterMotosOutputDTO>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Ocorreu um erro ao processar a requisição")]
        [SwaggerResponseExample(200, typeof(MotosOutputSample))]
        [EnableRateLimiting("rateLimit")]
        //IEnumerable<ObterMotosOutputDTO>
        [HttpGet("patios/{id}")]
        public IActionResult ObterTodasAsMotosDoPatio(string id, int pagina = 1, int tamanho = 5)
        {
            var motos = _motoService.ObterTodasAsMotosDoPatio(id);

            var totalItens = motos.Count();
            var totalPaginas = (int)Math.Ceiling(totalItens / (double)tamanho);

            var paginaMotos = motos
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho);

            var output = new
            {
                data = _mapper.Map<IEnumerable<Moto>, IEnumerable<ObterMotosOutputDTO>>(motos),
                paginaAtual = pagina,
                tamanhoPagina = tamanho,
                totalItens,
                totalPaginas
            };

            return Ok(output);
        }

        [SwaggerOperation(
            Summary = "Obtém uma moto",
            Description = "Retorna uma motos pelo id fornecido"
        )]
        [SwaggerResponse(200, "Motos retornada com sucesso", typeof(ObterMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Não foi possível encontrar uma moto ou a localização com esse id")]
        [EnableRateLimiting("rateLimit")]
        [SwaggerResponseExample(200, typeof(MotoOutputSample))]
        //ObterMotoOutputDTO 
        [HttpGet("{id}")]
        public IActionResult ObterMotoPorId(string id)
        {
            var moto = _motoService.ObterMotoPorId(id);
            var localizacao = _localizacaoService.ObterLocalizacaoPeloId(id);

            if (moto is null || localizacao is null)
                return BadRequest("Não foi possível encontrar uma moto ou a localização com esse id");

            var localizacaoDTO = new LocalizacaoDTO(localizacao.IdMoto, localizacao.LatitudeMoto, localizacao.LongitudeMoto);

            var obterMoto = new ObterMotoOutputDTO
            (
                moto.IdMoto,
                moto.PlacaMoto,
                moto.ModeloMoto,
                moto.StatusMoto,
                moto.ChassiMoto,
                moto.IotMoto,
                moto.IdPatio,
                localizacaoDTO
            );

            return Ok(MontarHateoas(obterMoto, obterMoto.IdMoto));
        }

        [SwaggerOperation(
            Summary = "Cadastra uma moto",
            Description = "Cadastra uma moto e sua localização no sistema"
        )]
        [SwaggerResponse(201, "Moto cadastrada com sucesso", typeof(CadastrarMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Ocorreu um erro ao processar a requisição")]
        [SwaggerRequestExample(typeof(CadastrarMotoInputDTO), typeof(CriarMotoSample))]
        [SwaggerResponseExample(201, typeof(MotoOutputSample))]
        [EnableRateLimiting("rateLimit")]
        //CadastrarMotoOutputDTO 
        [HttpPost()]
        public IActionResult CadastrarMoto([FromBody] CadastrarMotoInputDTO dto)
        {
            var moto = _motoService.CadastrarMoto(_mapper.Map<CadastrarMotoInputDTO, Moto>(dto));

            var localizacao = _localizacaoService.CadastrarLocalizacaoDaMoto(moto.IdMoto);

            var output = _mapper.Map<Moto, CadastrarMotoOutputDTO>(moto);

            return CreatedAtAction(nameof(ObterMotoPorId), new { id = output.IdMoto }, MontarHateoas(output, output.IdMoto));
        }

        [SwaggerOperation(
            Summary = "Atualiza uma moto",
            Description = "Atualiza as informações de uma moto"
        )]
        [SwaggerResponse(200, "Moto atualizada com sucesso", typeof(AtualizarMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Moto não encontrada pelo id")]
        [SwaggerResponseExample(200, typeof(MotoOutputSample))]
        [EnableRateLimiting("rateLimit")]
        //AtualizarMotoOutputDTO 
        [HttpPut()]
        public IActionResult AtualizarMoto([FromBody] AtualizarMotoInputDTO dto)
        {
            var moto = _mapper.Map<AtualizarMotoInputDTO, Moto>(dto);
            var motoAtualizada = _motoService.AtualizarMoto(moto);

            if (motoAtualizada is null)
                return BadRequest("Moto não encontrada!");

            var output = _mapper.Map<Moto, AtualizarMotoOutputDTO>(motoAtualizada);

            return Ok(MontarHateoas(output, output.IdMoto));
        }


        [SwaggerOperation(
            Summary = "Deleta uma moto",
            Description = "Deleta a moto da aplicação"
        )]
        [SwaggerResponse(204, "Moto apagada com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Moto não encontrada pelo id")]
        [EnableRateLimiting("rateLimit")]
        [HttpDelete("{id}")]
        public IActionResult DeletarMotoPorId(string id)
        {
            Moto moto = _motoService.DeletarMotoPorId(id);
            if (moto is null)
                return BadRequest("Moto não encontrada!");

            return NoContent();
        }

        private object MontarHateoas<T>(T dto, string id)
        {
            return new
            {
                data = dto,
                links = new
                {
                    self = Url.Action(nameof(ObterMotoPorId), "Moto", new { id }, Request.Scheme),
                    put = Url.Action(nameof(AtualizarMoto), "Moto", null, Request.Scheme),
                    delete = Url.Action(nameof(DeletarMotoPorId), "Moto", new { id }, Request.Scheme)
                }
            };
        }
    }
}
