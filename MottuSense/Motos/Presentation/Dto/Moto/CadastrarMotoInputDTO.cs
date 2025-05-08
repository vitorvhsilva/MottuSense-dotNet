using Motos.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
