using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Application.Services;
using Motos.Domain.Entitites;
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
            if (moto is null)
                return BadRequest("Não foi possível encontrar uma moto com esse id");

            var obterMoto = _mapper.Map<Moto, ObterMotoOutputDTO>(moto);

            return Ok(obterMoto);
        }


        //CadastrarMotoOutputDTO 
        [HttpPost]
        public IActionResult CadastrarMoto([FromBody] CadastrarMotoInputDTO dto)
        {
            var moto = _motoService.CadastrarMoto(_mapper.Map<CadastrarMotoInputDTO, Moto>(dto));

            _localizacaoService.CadastrarLocalizacaoDaMoto(moto.IdMoto);

            var output = _mapper.Map<Moto, CadastrarMotoOutputDTO>(moto);

            return CreatedAtAction(nameof(ObterMotoPorId), new { id = output.IdMoto }, output);
        }

        //AtualizarMotoOutputDTO 
        [HttpPut("{id}")]
        public IActionResult AtualizarMoto([FromBody] AtualizarMotoInputDTO dto)
        {
            return Ok();
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
