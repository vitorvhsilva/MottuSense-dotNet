using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.EventoMoto;


namespace Motos.Presentation.Controllers
{
    [Route("api/v1/eventos")]
    [ApiController]
    public class EventoMotoController : ControllerBase
    {
        private readonly IEventoMotoService _service;
        private readonly IMapper _mapper;

        public EventoMotoController(IEventoMotoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //CadastrarEventoMotoOutputDTO
        [HttpPost]
        public IActionResult Post([FromBody] CadastrarEventoMotoInputDTO dto)
        {
            var evento = _mapper.Map<CadastrarEventoMotoInputDTO, EventoMoto>(dto);
            var eventoPublicado = _service.PublicarEvento(evento);

            return Ok(_mapper.Map<EventoMoto, CadastrarEventoMotoOutputDTO>(eventoPublicado));
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
