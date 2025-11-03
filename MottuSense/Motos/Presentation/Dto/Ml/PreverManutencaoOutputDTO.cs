namespace Motos.Presentation.Dto.Ml
{
    public record PreverManutencaoOutputDTO(
        string IdMoto,
        bool ManutencaoPrevista,
        float Probabilidade
    );
}
