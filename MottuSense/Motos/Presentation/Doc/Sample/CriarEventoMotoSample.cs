using Swashbuckle.AspNetCore.Filters;

namespace Motos.Presentation.Doc.Sample
{
    public class CriarEventoMotoSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new
            {
                IdMoto = "idMoto",
                IdEvento = 1,
                DataHoraEvento = DateTime.UtcNow
            };
        }
    }
}
