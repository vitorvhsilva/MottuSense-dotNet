using Motos.Domain.Entitites;
using Motos.Domain.Interfaces;
using Motos.Infraestructure.Data.AppData;

namespace Motos.Infraestructure.Data.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly ApplicationContext _context;

        public LocalizacaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public LocalizacaoMoto CadastrarLocalizacaoDaMoto(LocalizacaoMoto localizacao)
        {
            _context.LocalizacaoMoto.Add(localizacao);
            _context.SaveChanges();

            return localizacao;
        }

        public LocalizacaoMoto ObterLocalizacaoPeloId(string IdMoto)
        {
            return _context.LocalizacaoMoto.FirstOrDefault(l => l.IdMoto == IdMoto);
        }
    }
}
