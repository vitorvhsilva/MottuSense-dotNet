using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Domain.Interfaces;

namespace Motos.Application.Services
{
    public class LocalizacaoService : ILocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public LocalizacaoMoto CadastrarLocalizacaoDaMoto(string IdMoto)
        {
            var localizacao = new LocalizacaoMoto
            {
                IdMoto = IdMoto,
                LatitudeMoto = "NAO_INFORMADO",
                LongitudeMoto = "NAO_INFORMADO"
            };

            _repository.CadastrarLocalizacaoDaMoto(localizacao);
            return localizacao;
        }

        public LocalizacaoMoto ObterLocalizacaoPeloId(string IdMoto)
        {
            return _repository.ObterLocalizacaoPeloId(IdMoto);
        }
    }
}
