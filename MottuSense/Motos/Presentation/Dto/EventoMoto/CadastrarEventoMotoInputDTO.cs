namespace Motos.Presentation.Dto.EventoMoto
{
    public record CadastrarEventoMotoInputDTO
    (
        string IdMoto,
        int IdEvento,
        DateTime DataHoraEvento
    );
}
