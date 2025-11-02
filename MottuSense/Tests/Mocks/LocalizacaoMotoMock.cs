using Motos.Domain.Entities;

namespace Motos.Tests.Mocks
{
    public static class LocalizacaoMotoMock
    {
        public static LocalizacaoMoto Criar(
            string idMoto = "moto-001",
            string latitude = "0.0",
            string longitude = "0.0")
        {
            return new LocalizacaoMoto
            {
                IdMoto = idMoto,
                LatitudeMoto = latitude,
                LongitudeMoto = longitude
            };
        }
        public static List<LocalizacaoMoto> CriarLista(int quantidade = 3)
        {
            var lista = new List<LocalizacaoMoto>();
            for (int i = 1; i <= quantidade; i++)
            {
                lista.Add(Criar(
                    idMoto: $"moto-{i:000}",
                    latitude: (i * 10.0).ToString(),
                    longitude: (i * 20.0).ToString()
                ));
            }
            return lista;
        }
    }
}
