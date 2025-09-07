using Swashbuckle.AspNetCore.Filters;
namespace Motos.Presentation.Doc.Sample
{
    public class EventoMotosOutputSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            var eventos = new[]
            {
                new
                {
                    data = new
                    {
                        IdEventoMoto = "EV123",
                        IdMoto = "MOTO45",
                        IdEvento = 101,
                        EventoVisualizado = false,
                        DataHoraEvento = DateTime.UtcNow
                    },
                    links = new
                    {
                        self = "https://localhost:5001/api/v1/eventos/EV123",
                        getByMoto = "https://localhost:5001/api/v1/eventos/motos/MOTO45",
                        getByPatio = "https://localhost:5001/api/v1/eventos/patios/PATIO99",
                        visualizar = "https://localhost:5001/api/v1/eventos/visualizar"
                    }
                },
                new
                {
                    data = new
                    {
                        IdEventoMoto = "EV124",
                        IdMoto = "MOTO46",
                        IdEvento = 102,
                        EventoVisualizado = true,
                        DataHoraEvento = DateTime.UtcNow.AddMinutes(-10)
                    },
                    links = new
                    {
                        self = "https://localhost:5001/api/v1/eventos/EV124",
                        getByMoto = "https://localhost:5001/api/v1/eventos/motos/MOTO46",
                        getByPatio = "https://localhost:5001/api/v1/eventos/patios/PATIO99",
                        visualizar = "https://localhost:5001/api/v1/eventos/visualizar"
                    }
                }
            };

            return new
            {
                data = eventos,
                paginaAtual = 1,
                tamanhoPagina = 2,
                totalItens = 10,
                totalPaginas = 5
            };
        }
    }
}
