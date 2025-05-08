using Motos.Domain.Entities.Enums;

namespace Motos.Presentation.Dto.Output
{
    public record ObterMotoOutputDTO(
       string IdMoto,
       string PlacaMoto,
       ModeloMoto ModeloMoto,
       StatusMoto StatusMoto,
       string ChassiMoto,
       string IotMoto,
       string IdPatio
    );
}
