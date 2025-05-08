using Motos.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Motos.Presentation.Dto.Moto
{
    public record CadastrarMotoInputDTO(
        string PlacaMoto,
        ModeloMoto ModeloMoto,
        StatusMoto StatusMoto,
        string ChassiMoto,
        string IotMoto,
        string IdPatio
    );
}
