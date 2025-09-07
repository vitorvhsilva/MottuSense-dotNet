namespace Motos.Presentation.Doc.Sample
{
    using Swashbuckle.AspNetCore.Filters;

    public class EventoMotoOutputSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                data = new
                {
                    IdEventoMoto = "EV123",
                    IdMoto = "MOTO45",
                    IdEvento = 101,
                    EventoVisualizado = false,
                    DataHoraEvento = DateTime.UtcNow,
                },
                links = new
                {
                    self = "https://localhost:5001/api/v1/eventos/EV123",
                    getByMoto = "https://localhost:5001/api/v1/eventos/motos/MOTO45",
                    getByPatio = "https://localhost:5001/api/v1/eventos/patios/PATIO99",
                    visualizar = "https://localhost:5001/api/v1/eventos/visualizar"
                }
            };
        }
    }
}
