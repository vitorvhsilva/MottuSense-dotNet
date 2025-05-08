using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entitites;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Mapper;

namespace Motos.Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _service;

        public MotoController(IMotoService service)
        {
            _service = service;
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

            return Created();
        }

        //AtualizarMotoOutputDTO 
        [HttpPut("{id}")]
        public IActionResult AtualizarMoto([FromBody] AtualizarMotoInputDTO dto)
        {
            return Ok();
        }
    }
}
