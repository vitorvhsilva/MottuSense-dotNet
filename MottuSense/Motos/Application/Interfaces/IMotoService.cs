using Motos.Domain.Entitites;

namespace Motos.Application.Interfaces
{
    public interface IMotoService
    {
        IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id);
        Moto ObterMotoPorId(string id);
        Moto AtualizarMoto(Moto dto);
        Moto CadastrarMoto(Moto dto);
        Moto DeletarMotoPorId(string IdMoto);
    }
}
