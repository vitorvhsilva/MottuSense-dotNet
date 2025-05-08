using Motos.Domain.Entities.Enums;

namespace Motos.Presentation.Dto.Output
{
    public record ObterMotosOutputDTO(
       string PlacaMoto,
       ModeloMoto ModeloMoto,
       StatusMoto StatusMoto,
       string IdPatio
    );
}
