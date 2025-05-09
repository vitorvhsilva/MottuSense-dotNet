namespace Motos.Presentation.Dto.EventoMoto
{
    public record CadastrarEventoMotoOutputDTO
    (
        string IdEventoMoto,
        string IdMoto,
        int IdEvento,
        bool EventoVisualizado,
        DateTime DataHoraEvento
    );
}
