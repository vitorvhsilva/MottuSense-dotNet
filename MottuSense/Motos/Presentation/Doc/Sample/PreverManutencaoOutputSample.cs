using Motos.Presentation.Dto.Ml;

namespace Motos.Presentation.Doc.Sample
{
    public class PreverManutencaoOutputSample : Swashbuckle.AspNetCore.Filters.IExamplesProvider<PreverManutencaoOutputDTO>
    {
        public PreverManutencaoOutputDTO GetExamples()
        {
            return new PreverManutencaoOutputDTO(
                "moto-001",
                true,
                0.85f
            );
        }
    }
}
