using Motos.Presentation.Dto.Ml;

namespace Motos.Presentation.Doc.Sample
{
    public class PreverManutencaoInputSample : Swashbuckle.AspNetCore.Filters.IExamplesProvider<PreverManutencaoInputDTO>
    {
        public PreverManutencaoInputDTO GetExamples()
        {
            return new PreverManutencaoInputDTO
            (
                "moto-001",
                "MOTTU_E",
                "PRONTA_PARA_ALUGUEL",
                "patio-01",
                1
            );
        }
    }
}
