using Motos.Domain.Entities.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Motos.Presentation.Doc.Sample
{
    public class AtualizarMotoSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                IdMoto = "MOTO123",
                PlacaMoto = "XYZ-9876",
                ModeloMoto = ModeloMoto.MOTTU_E,
                StatusMoto = StatusMoto.AGENDADA_PARA_MANUTENCAO, 
                ChassiMoto = "9BWZZZ377VT004252",
                IotMoto = "IOT002",
                IdPatio = "PATIO45"
            };
        }
    }
}
