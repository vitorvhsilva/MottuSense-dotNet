namespace Motos.Presentation.Dto.Ml
{
    public record PreverManutencaoInputDTO(
        string IdMoto,
        string ModeloMoto,
        string StatusMoto,
        string IdPatio,
        int QtdEventosRecentes
    );
}
