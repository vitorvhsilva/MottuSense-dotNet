using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entitites;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Mappers;

namespace Motos.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _service;
        private readonly IMapper _mapper;

        public MotoController(IMotoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        //IEnumerable<ObterMotosOutputDTO>
        [HttpGet("patios/{id}")]
        public IActionResult ObterTodasAsMotosDoPatio(string id)
        {
            IEnumerable<Moto> motos = _service.ObterTodasAsMotosDoPatio(id);

            return Ok();
        }

        //ObterMotoOutputDTO 
        [HttpGet("{id}")]
        public IActionResult ObterMotoPorId(string id)
        {
            Moto moto = _service.ObterMotoPorId(id);
            
            return Ok();
        }


        //CadastrarMotoOutputDTO 
        [HttpPost]
        public IActionResult CadastrarMoto([FromBody] CadastrarMotoInputDTO dto)
        {
            Moto moto = _service.CadastrarMoto(_mapper.Map<Moto>(dto));

            CadastrarMotoOutputDTO output = _mapper.Map<CadastrarMotoOutputDTO>(moto);

            return CreatedAtAction(nameof(ObterMotoPorId), new { id = output.IdMoto }, output);
        }

        //AtualizarMotoOutputDTO 
        [HttpPut("{id}")]
        public IActionResult AtualizarMoto([FromBody] AtualizarMotoInputDTO dto)
        {
            return Ok();
        }
    }
}
