using Motos.Domain.Entities.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Motos.Presentation.Doc.Sample
{
    public class MotoOutputSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                data = new
                {
                    IdMoto = "MOTO123",
                    PlacaMoto = "ABC-1234",
                    ModeloMoto = ModeloMoto.MOTTU_E,
                    StatusMoto = StatusMoto.AGENDADA_PARA_MANUTENCAO, 
                    ChassiMoto = "9BWZZZ377VT004251",
                    IotMoto = "IOT001",
                    IdPatio = "PATIO45",
                    Localizacao = new
                    {
                        IdMoto = "MOTO123",
                        Latitude = -23.55052,
                        Longitude = -46.633308
                    }
                },
                links = new
                {
                    self = "https://localhost:5001/api/v1/motos/MOTO123",
                    put = "https://localhost:5001/api/v1/motos",
                    delete = "https://localhost:5001/api/v1/motos/MOTO123"
                }
            };
        }
    }
}
