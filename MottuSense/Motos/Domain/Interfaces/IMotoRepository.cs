using Motos.Domain.Entitites;

namespace Motos.Domain.Interfaces
{
    public interface IMotoRepository
    {
        IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id);
        Moto ObterMotoPorId(string id);
        Moto AtualizarMoto();
        Moto CadastrarMoto();
    }
}
