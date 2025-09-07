using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Doc.Sample;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Presentation.Dto.Output;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
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
        [SwaggerResponse(201, "Evento criado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Ocorreu um erro ao processar a requisição")]
        [SwaggerRequestExample(typeof(CadastrarEventoMotoInputDTO), typeof(CriarEventoMotoSample))]
        [SwaggerResponseExample(201, typeof(EventoMotoOutputSample))]
        [EnableRateLimiting("rateLimit")]

        //CadastrarEventoMotoOutputDTO
        [HttpPost]
        public IActionResult Post([FromBody] CadastrarEventoMotoInputDTO dto)
        {
            var evento = _mapper.Map<CadastrarEventoMotoInputDTO, EventoMoto>(dto);
            var eventoPublicado = _service.PublicarEvento(evento);
            var eventoOutput = _mapper.Map<EventoMoto, ObterEventoMotoDTO>(eventoPublicado);

            return CreatedAtAction(nameof(PegarEventoPorIdEventoMoto), 
                new { IdEventoMoto = eventoOutput.IdEventoMoto }, MontarHateoas(eventoOutput, eventoOutput.IdEventoMoto));
        }

        [SwaggerOperation(
             Summary = "Obtém um evento pelo IdEventoMoto",
             Description = "Retorna um evento e suas informações pelo seu Id de criação"
         )]
        [SwaggerResponse(200, "Evento retornado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Evento não encontrado pelo id")]
        [SwaggerResponseExample(200, typeof(EventoMotoOutputSample))]
        [EnableRateLimiting("rateLimit")]
        //ObterEventoMotoDTO
        [HttpGet("{IdEventoMoto}")]
        public IActionResult PegarEventoPorIdEventoMoto(string IdEventoMoto)
        {
            var evento = _service.PegarEventoPorIdEventoMoto(IdEventoMoto);
            if (evento is null)
                return BadRequest("Evento não encontrado pelo id");

            var eventoOutput = _mapper.Map<EventoMoto, ObterEventoMotoDTO>(evento);
            return Ok(MontarHateoas(eventoOutput, eventoOutput.IdEventoMoto));
        }

        [SwaggerOperation(
             Summary = "Obtém um evento pelo IdMoto",
             Description = "Retorna um evento e suas informações pelo seu Id da Moto"
         )]
        [SwaggerResponse(200, "Evento retornado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Evento não encontrado pelo id da moto")]
        [SwaggerResponseExample(200, typeof(EventoMotosOutputSample))]
        [EnableRateLimiting("rateLimit")]
        //IEnumerable<ObterEventoMotoDTO>
        [HttpGet("motos/{IdMoto}")]
        public IActionResult PegarEventosPorIdMoto(string IdMoto, int pagina = 1, int tamanho = 5)
        {
            var eventos = _service.PegarEventosPorIdMoto(IdMoto);
            if (eventos.Count().Equals(0))
                return BadRequest("Nenhum evento encontrado pelo id da moto");

            return Ok(PaginarEventos(eventos, pagina, tamanho));
        }

        [SwaggerOperation(
             Summary = "Obtém um evento pelo IdPatio",
             Description = "Retorna um evento e suas informações pelo seu Id do Patio"
         )]
        [SwaggerResponse(200, "Evento retornado com sucesso", typeof(ObterEventoMotoDTO))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Evento não encontrado pelo id do pátio")]
        [SwaggerResponseExample(200, typeof(EventoMotosOutputSample))]
        [EnableRateLimiting("rateLimit")]
        //IEnumerable<ObterEventoMotoDTO>
        [HttpGet("patios/{IdPatio}")]
        public IActionResult PegarEventosPorIdPatio(string IdPatio, int pagina = 1, int tamanho = 5)
        {
            var eventos = _service.PegarEventosPorIdPatio(IdPatio);
            if (eventos.Count().Equals(0))
                return BadRequest("Nenhum evento encontrado pelo id do patio");

            return Ok(PaginarEventos(eventos, pagina, tamanho));
        }

        [SwaggerOperation(
             Summary = "Visualiza os Eventos",
             Description = "Visualiza uma lista de Eventos pelo id"
         )]
        [SwaggerResponse(200, "Eventos visualizados com sucesso")]
        [EnableRateLimiting("rateLimit")]
        [HttpPatch("visualizar")]
        public IActionResult VisualizarEventos([FromBody] VisualizarEventosDTO IdEventos)
        {
            _service.MarcarEventosComoVisualizado(IdEventos);
            return Ok();
        }

        private object PaginarEventos(IEnumerable<EventoMoto> eventos, int pagina, int tamanho)
        {
            var totalItens = eventos.Count();
            var totalPaginas = (int)Math.Ceiling(totalItens / (double)tamanho);

            var paginaEventos = eventos
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho);

            var eventosComLinks = _mapper
                .Map<IEnumerable<EventoMoto>, IEnumerable<ObterEventoMotoDTO>>(paginaEventos)
                .Select(e => MontarHateoas(e, e.IdEventoMoto));

            return new
            {
                data = eventosComLinks,
                paginaAtual = pagina,
                tamanhoPagina = tamanho,
                totalItens,
                totalPaginas
            };
        }

        private object MontarHateoas<T>(T dto, string id)
        {
            return new
            {
                data = dto,
                links = new
                {
                    self = Url.Action(nameof(PegarEventoPorIdEventoMoto), "EventoMoto", new { IdEventoMoto = id }, Request.Scheme),
                    getByMoto = Url.Action(nameof(PegarEventosPorIdMoto), "EventoMoto", new { IdMoto = "" }, Request.Scheme),
                    getByPatio = Url.Action(nameof(PegarEventosPorIdPatio), "EventoMoto", new { IdPatio = "" }, Request.Scheme),
                    visualizar = Url.Action(nameof(VisualizarEventos), "EventoMoto", null, Request.Scheme)
                }
            };
        }
    }
}
