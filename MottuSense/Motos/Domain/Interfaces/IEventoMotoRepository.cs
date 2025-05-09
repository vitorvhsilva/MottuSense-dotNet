using Motos.Domain.Entities;

namespace Motos.Domain.Interfaces
{
    public interface IEventoMotoRepository
    {
        EventoMoto PublicarEvento(EventoMoto evento);
        void VisualizarEvento(IEnumerable<String> ids);
        EventoMoto PegarEventoPorIdEvento(string IdEventoMoto);
        IEnumerable<EventoMoto> PegarEventosPorIdMoto(string IdMoto);
        IEnumerable<EventoMoto> PegarEventosPorIdPatio(string IdMoto);
        bool ExisteEventoPorIdEvento(string IdEventoMoto);
    }
}
