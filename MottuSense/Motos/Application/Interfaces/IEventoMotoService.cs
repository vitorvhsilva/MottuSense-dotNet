using Motos.Domain.Entities;

namespace Motos.Application.Interfaces
{
    public interface IEventoMotoService
    {
        EventoMoto PublicarEvento(EventoMoto evento);
        void MarcarEventosComoVisualizado(IEnumerable<string> ids);
        EventoMoto PegarEventoPorIdEvento(string IdEventoMoto);
        IEnumerable<EventoMoto> PegarEventosPorIdMoto(string IdMoto);
        IEnumerable<EventoMoto> PegarEventosPorIdPatio(string IdPatio);
    }
}
