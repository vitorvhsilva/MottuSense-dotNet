using Motos.Domain.Entitites;
using Motos.Presentation.Dto.Moto;

namespace Motos.Application.Interfaces
{
    public interface IMotoService
    {
        IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id);
        Moto ObterMotoPorId(string id);
        Moto AtualizarMoto(AtualizarMotoInputDTO dto);
        Moto CadastrarMoto(CadastrarMotoInputDTO dto);
    }
}
