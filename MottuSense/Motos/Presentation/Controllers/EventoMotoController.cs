using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Presentation.Dto.Output;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


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

        [SwaggerOperation(
            Summary = "Cadastra um evento",
            Description = "Cadastra um evento na aplicação"
        )]
        [SwaggerResponse(201, "Evento criado com sucesso", typeof(CadastrarEventoMotoOutputDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Ocorreu um erro ao processar a requisição")]

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

        [SwaggerOperation(
             Summary = "Obtém um evento pelo IdEventoMoto",
             Description = "Retorna um evento e suas informações pelo seu Id de criação"
         )]
        [SwaggerResponse(200, "Evento retornado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Evento não encontrado pelo id")]
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

        [SwaggerOperation(
             Summary = "Obtém um evento pelo IdMoto",
             Description = "Retorna um evento e suas informações pelo seu Id da Moto"
         )]
        [SwaggerResponse(200, "Evento retornado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Evento não encontrado pelo id da moto")]

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

        [SwaggerOperation(
             Summary = "Obtém um evento pelo IdPatio",
             Description = "Retorna um evento e suas informações pelo seu Id do Patio"
         )]
        [SwaggerResponse(200, "Evento retornado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Evento não encontrado pelo id do pátio")]

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

        [SwaggerOperation(
             Summary = "Visualiza os Eventos",
             Description = "Visualiza uma lista de Eventos pelo id"
         )]
        [SwaggerResponse(200, "Eventos visualizados com sucesso")]

        //void
        [HttpPatch("visualizar")]
        public IActionResult VisualizarEventos([FromBody] VisualizarEventosDTO IdEventos)
        {
            _service.MarcarEventosComoVisualizado(IdEventos);
            return Ok();
        }
    }
}
