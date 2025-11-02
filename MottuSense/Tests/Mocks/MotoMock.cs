using Motos.Domain.Entities;
using Motos.Domain.Entities.Enums;
using System.Collections.Generic;

namespace Motos.Tests.Mocks
{
    public static class MotoMock
    {
        public static Moto CriarMotoPadrao()
        {
            return new Moto
            {
                IdMoto = Guid.NewGuid().ToString(),
                PlacaMoto = "ABC1D23",
                ModeloMoto = ModeloMoto.MOTTU_E,
                StatusMoto = StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto = "9C2PC4010XR000001",
                IotMoto = "iot-123",
                IdPatio = "patio-01"
            };
        }

        public static Moto CriarMotoSemId()
        {
            return new Moto
            {
                PlacaMoto = "XYZ9A88",
                ModeloMoto = ModeloMoto.MOTTU_E,
                StatusMoto = StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto = "9BWZZZ377VT004251",
                IotMoto = "iot-456",
                IdPatio = "patio-02"
            };
        }

        public static List<Moto> CriarListaMotos()
        {
            return new List<Moto>
            {
                new Moto { IdMoto = "1", PlacaMoto = "AAA1111", ModeloMoto = ModeloMoto.MOTTU_E, StatusMoto = StatusMoto.PRONTA_PARA_ALUGUEL, IdPatio = "p1" },
                new Moto { IdMoto = "2", PlacaMoto = "BBB2222", ModeloMoto = ModeloMoto.MOTTU_E, StatusMoto = StatusMoto.PRONTA_PARA_ALUGUEL, IdPatio = "p1" }
            };
        }
    }
}
