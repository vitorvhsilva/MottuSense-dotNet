using Motos.Domain.Entitites;

namespace Motos.Application.Interfaces
{
    public interface ILocalizacaoService
    {
        void CadastrarLocalizacaoDaMoto(string IdMoto);
        LocalizacaoMoto ObterLocalizacaoPeloId(string IdMoto);
    }
}
