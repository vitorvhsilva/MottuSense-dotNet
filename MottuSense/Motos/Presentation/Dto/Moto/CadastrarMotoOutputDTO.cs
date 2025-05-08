using Motos.Domain.Entities.Enums;

namespace Motos.Presentation.Dto.Moto
{
    public record CadastrarMotoOutputDTO(
        string IdMoto,
        string PlacaMoto,
        ModeloMoto ModeloMoto,
        StatusMoto StatusMoto,
        string ChassiMoto,
        string IotMoto,
        string IdPatio
    );
}
