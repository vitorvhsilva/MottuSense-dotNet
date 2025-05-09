using Motos.Domain.Entities;
using Motos.Presentation.Dto.EventoMoto;

namespace Motos.Application.Interfaces
{
    public interface IEventoMotoService
    {
        EventoMoto PublicarEvento(EventoMoto evento);
        void MarcarEventosComoVisualizado(VisualizarEventosDTO dto);
        EventoMoto PegarEventoPorIdEventoMoto(string IdEventoMoto);
        IEnumerable<EventoMoto> PegarEventosPorIdMoto(string IdMoto);
        IEnumerable<EventoMoto> PegarEventosPorIdPatio(string IdPatio);
    }
}
