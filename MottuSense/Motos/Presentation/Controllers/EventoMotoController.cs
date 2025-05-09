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

        [HttpGet("{id}")]
        public IActionResult PegarEventoPorIdEventoMoto(string id)
        {
            var evento = _service.PegarEventoPorIdEventoMoto(id);
            if (evento is null)
                return BadRequest("Evento não encontrado pelo id");

            var eventoOutput = _mapper.Map<EventoMoto, ObterEventoMotoDTO>(evento);
            return Ok(eventoOutput);
        }


        //void
        [HttpPatch("visualizar")]
        public IActionResult VisualizarEventos([FromBody] VisualizarEventosDTO IdEventos)
        {
            _service.MarcarEventosComoVisualizado(IdEventos);
            return Ok();
        }
    }
}
