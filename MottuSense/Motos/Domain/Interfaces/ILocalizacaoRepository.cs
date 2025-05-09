using Motos.Domain.Entitites;

namespace Motos.Domain.Interfaces
{
    public interface ILocalizacaoRepository
    {
        void CadastrarLocalizacaoDaMoto(LocalizacaoMoto localizacao);
        LocalizacaoMoto ObterLocalizacaoPeloId(string IdMoto);
    }
}
