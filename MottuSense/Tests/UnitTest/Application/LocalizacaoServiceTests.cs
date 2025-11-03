using Moq;
using Motos.Application.Services;
using Motos.Domain.Entities;
using Motos.Domain.Interfaces;

namespace Motos.Tests.UnitTest.Application
{
    public class LocalizacaoServiceTests
    {
        private readonly Mock<ILocalizacaoRepository> _repositoryMock;
        private readonly LocalizacaoService _service;

        public LocalizacaoServiceTests()
        {
            _repositoryMock = new Mock<ILocalizacaoRepository>();
            _service = new LocalizacaoService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Deve cadastrar localização da moto com valores padrão")]
        public void CadastrarLocalizacaoDaMoto_DeveRetornarLocalizacaoComValoresPadrao()
        {
            var idMoto = "moto-001";

            LocalizacaoMoto? result = null;
            _repositoryMock.Setup(r => r.CadastrarLocalizacaoDaMoto(It.IsAny<LocalizacaoMoto>()))
                           .Callback<LocalizacaoMoto>(loc => result = loc);

            var retorno = _service.CadastrarLocalizacaoDaMoto(idMoto);

            Assert.NotNull(retorno);
            Assert.Equal(idMoto, retorno.IdMoto);
            Assert.Equal("NAO_INFORMADO", retorno.LatitudeMoto);
            Assert.Equal("NAO_INFORMADO", retorno.LongitudeMoto);

            _repositoryMock.Verify(r => r.CadastrarLocalizacaoDaMoto(It.IsAny<LocalizacaoMoto>()), Times.Once);

            Assert.Equal(result, retorno);
        }

        [Fact(DisplayName = "Deve obter localização da moto pelo Id")]
        public void ObterLocalizacaoPeloId_DeveRetornarLocalizacao()
        {
            var idMoto = "moto-001";
            var localizacaoEsperada = new LocalizacaoMoto
            {
                IdMoto = idMoto,
                LatitudeMoto = "10.0",
                LongitudeMoto = "20.0"
            };

            _repositoryMock.Setup(r => r.ObterLocalizacaoPeloId(idMoto))
                           .Returns(localizacaoEsperada);

            var resultado = _service.ObterLocalizacaoPeloId(idMoto);

            Assert.NotNull(resultado);
            Assert.Equal(localizacaoEsperada.IdMoto, resultado.IdMoto);
            Assert.Equal(localizacaoEsperada.LatitudeMoto, resultado.LatitudeMoto);
            Assert.Equal(localizacaoEsperada.LongitudeMoto, resultado.LongitudeMoto);

            _repositoryMock.Verify(r => r.ObterLocalizacaoPeloId(idMoto), Times.Once);
        }
    }
}
