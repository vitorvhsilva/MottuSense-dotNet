namespace Motos.Presentation.Dto.EventoMoto
{
    public record ObterEventoMotoDTO
    (
        string IdEventoMoto,
        string IdMoto,
        int IdEvento,
        bool EventoVisualizado,
        DateTime DataHoraEvento
    );
}
