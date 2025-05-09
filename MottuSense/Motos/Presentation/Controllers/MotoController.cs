using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.Localizacao;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Dto.Output;
using Swashbuckle.AspNetCore.Annotations;
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

        //IEnumerable<ObterMotosOutputDTO>
        [HttpGet("patios/{id}")]
        public IActionResult ObterTodasAsMotosDoPatio(string id)
        {
            var motos = _motoService.ObterTodasAsMotosDoPatio(id);

            var output = _mapper.Map<IEnumerable<Moto>, IEnumerable<ObterMotosOutputDTO>>(motos);

            return Ok(output);
        }

        [SwaggerOperation(
            Summary = "Obtém uma moto",
            Description = "Retorna uma motos pelo id fornecido"
        )]
        [SwaggerResponse(200, "Motos retornada com sucesso", typeof(ObterMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Não foi possível encontrar uma moto ou a localização com esse id")]

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

            return Ok(obterMoto);
        }

        [SwaggerOperation(
            Summary = "Cadastra uma moto",
            Description = "Cadastra uma moto e sua localização no sistema"
        )]
        [SwaggerResponse(201, "Moto cadastrada com sucesso", typeof(CadastrarMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Ocorreu um erro ao processar a requisição")]

        //CadastrarMotoOutputDTO 
        [HttpPost()]
        public IActionResult CadastrarMoto([FromBody] CadastrarMotoInputDTO dto)
        {
            var moto = _motoService.CadastrarMoto(_mapper.Map<CadastrarMotoInputDTO, Moto>(dto));

            var localizacao = _localizacaoService.CadastrarLocalizacaoDaMoto(moto.IdMoto);

            var output = _mapper.Map<Moto, CadastrarMotoOutputDTO>(moto);

            return CreatedAtAction(nameof(ObterMotoPorId), new { id = output.IdMoto }, output);
        }

        [SwaggerOperation(
            Summary = "Atualiza uma moto",
            Description = "Atualiza as informações de uma moto"
        )]
        [SwaggerResponse(200, "Moto atualizada com sucesso", typeof(AtualizarMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Moto não encontrada pelo id")]

        //AtualizarMotoOutputDTO 
        [HttpPut()]
        public IActionResult AtualizarMoto([FromBody] AtualizarMotoInputDTO dto)
        {
            var moto = _mapper.Map<AtualizarMotoInputDTO, Moto>(dto);
            var motoAtualizada = _motoService.AtualizarMoto(moto);

            if (motoAtualizada is null)
                return BadRequest("Moto não encontrada!");

            return Ok(_mapper.Map<Moto, AtualizarMotoOutputDTO>(motoAtualizada));
        }


        [SwaggerOperation(
            Summary = "Deleta uma moto",
            Description = "Deleta a moto da aplicação"
        )]
        [SwaggerResponse(204, "Moto apagada com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Moto não encontrada pelo id")]

        [HttpDelete("{id}")]
        public IActionResult DeletarMotoPorId(string id)
        {
            Moto moto = _motoService.DeletarMotoPorId(id);
            if (moto is null)
                return BadRequest("Moto não encontrada!");

            return NoContent();
        }
    }
}
