using Swashbuckle.AspNetCore.Filters;

namespace Motos.Presentation.Doc.Sample
{
    public class CriarEventoMotoSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                IdMoto = "MOTO45",
                IdEvento = 101,
                DataHoraEvento = DateTime.UtcNow
            };
        }
    }
}
