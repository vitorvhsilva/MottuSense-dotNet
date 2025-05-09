using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Domain.Interfaces;
using Motos.Presentation.Dto.EventoMoto;

namespace Motos.Application.Services
{
    public class EventoMotoService : IEventoMotoService
    {
        private readonly IEventoMotoRepository _repository;

        public EventoMotoService(IEventoMotoRepository repository)
        {
            _repository = repository;
        }

        public void MarcarEventosComoVisualizado(VisualizarEventosDTO dto)
        {
            foreach (var IdEvento in dto.IdEventos)
            {
                _repository.VisualizarEvento(IdEvento);
            }
        }

        public EventoMoto PegarEventoPorIdEvento(string IdEventoMoto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventoMoto> PegarEventosPorIdMoto(string IdMoto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventoMoto> PegarEventosPorIdPatio(string IdPatio)
        {
            throw new NotImplementedException();
        }

        public EventoMoto PublicarEvento(EventoMoto evento)
        {
            string IdEventoMoto = null;
            do
            {
                IdEventoMoto = Guid.NewGuid().ToString();
            } while (_repository.ExisteEventoPorIdEvento(IdEventoMoto));

            evento.IdEventoMoto = IdEventoMoto;
            evento.EventoVisualizado = false;

            return _repository.PublicarEvento(evento);
        }
    }
}
