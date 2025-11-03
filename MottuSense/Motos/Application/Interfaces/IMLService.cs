using Motos.Presentation.Dto.Ml;

namespace Motos.Application.Interfaces
{
    public interface IMLService
    {
        public PreverManutencaoOutputDTO Prever(PreverManutencaoInputDTO dto);
    }
}
