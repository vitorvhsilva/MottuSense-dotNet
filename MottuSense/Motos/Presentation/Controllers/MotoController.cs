using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.Localizacao;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Dto.Output;

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


        //IEnumerable<ObterMotosOutputDTO>
        [HttpGet("patios/{id}")]
        public IActionResult ObterTodasAsMotosDoPatio(string id)
        {
            var motos = _motoService.ObterTodasAsMotosDoPatio(id);

            var output = _mapper.Map<IEnumerable<Moto>, IEnumerable<ObterMotosOutputDTO>>(motos);

            return Ok(output);
        }

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


        //CadastrarMotoOutputDTO 
        [HttpPost]
        public IActionResult CadastrarMoto([FromBody] CadastrarMotoInputDTO dto)
        {
            var moto = _motoService.CadastrarMoto(_mapper.Map<CadastrarMotoInputDTO, Moto>(dto));

            var localizacao = _localizacaoService.CadastrarLocalizacaoDaMoto(moto.IdMoto);

            var output = _mapper.Map<Moto, CadastrarMotoOutputDTO>(moto);

            return CreatedAtAction(nameof(ObterMotoPorId), new { id = output.IdMoto }, output);
        }

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
