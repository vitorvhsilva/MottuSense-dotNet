using Motos.Domain.Entitites;
using Motos.Presentation.Dto.Moto;

namespace Motos.Domain.Interfaces
{
    public interface IMotoRepository
    {
        IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id);
        Moto ObterMotoPorId(string id);
        Moto ObterMotoPorPlaca(string placa);
        Moto AtualizarMoto(Moto moto);
        Moto CadastrarMoto(Moto moto);
    }
}
