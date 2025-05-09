using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Presentation.Dto.Output;


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
            var eventoOutput = _mapper.Map<EventoMoto, CadastrarEventoMotoOutputDTO>(eventoPublicado);

            return CreatedAtAction(nameof(PegarEventoPorIdEventoMoto), 
                new { IdEventoMoto = eventoOutput.IdEventoMoto }, eventoOutput);
        }

        //ObterEventoMotoDTO
        [HttpGet("{IdEventoMoto}")]
        public IActionResult PegarEventoPorIdEventoMoto(string IdEventoMoto)
        {
            var evento = _service.PegarEventoPorIdEventoMoto(IdEventoMoto);
            if (evento is null)
                return BadRequest("Evento não encontrado pelo id");

            var eventoOutput = _mapper.Map<EventoMoto, ObterEventoMotoDTO>(evento);
            return Ok(eventoOutput);
        }

        //IEnumerable<ObterEventoMotoDTO>
        [HttpGet("motos/{IdMoto}")]
        public IActionResult PegarEventosPorIdMoto(string IdMoto)
        {
            var eventos = _service.PegarEventosPorIdMoto(IdMoto);
            if (eventos.Count().Equals(0))
                return BadRequest("Nenhum evento encontrado pelo id da moto");

            var eventoOutput = _mapper.Map<IEnumerable<EventoMoto>, IEnumerable<ObterEventoMotoDTO>>(eventos);
            return Ok(eventoOutput);
        }

        //IEnumerable<ObterEventoMotoDTO>
        [HttpGet("patios/{IdPatio}")]
        public IActionResult PegarEventosPorIdPatio(string IdPatio)
        {
            var eventos = _service.PegarEventosPorIdPatio(IdPatio);
            if (eventos.Count().Equals(0))
                return BadRequest("Nenhum evento encontrado pelo id do patio");

            var eventoOutput = _mapper.Map<IEnumerable<EventoMoto>, IEnumerable<ObterEventoMotoDTO>>(eventos);
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
