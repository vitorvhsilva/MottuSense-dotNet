using Motos.Domain.Entities;

namespace Motos.Application.Interfaces
{
    public interface ILocalizacaoService
    {
        LocalizacaoMoto CadastrarLocalizacaoDaMoto(string IdMoto);
        LocalizacaoMoto ObterLocalizacaoPeloId(string IdMoto);
    }
}
