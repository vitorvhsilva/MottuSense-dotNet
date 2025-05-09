using Motos.Domain.Entities;

namespace Motos.Domain.Interfaces
{
    public interface ILocalizacaoRepository
    {
        LocalizacaoMoto CadastrarLocalizacaoDaMoto(LocalizacaoMoto localizacao);
        LocalizacaoMoto ObterLocalizacaoPeloId(string IdMoto);
    }
}
