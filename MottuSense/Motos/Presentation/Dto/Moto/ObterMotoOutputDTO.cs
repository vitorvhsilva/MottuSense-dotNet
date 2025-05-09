using Motos.Domain.Entities.Enums;
using Motos.Presentation.Dto.Localizacao;

namespace Motos.Presentation.Dto.Output
{
    public record ObterMotoOutputDTO(
       string IdMoto,
       string PlacaMoto,
       ModeloMoto ModeloMoto,
       StatusMoto StatusMoto,
       string ChassiMoto,
       string IotMoto,
       string IdPatio,
       LocalizacaoDTO Localizacao
    );
}
