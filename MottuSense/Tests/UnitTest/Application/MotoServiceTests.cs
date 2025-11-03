using Xunit;
using Moq;
using System.Collections.Generic;
using Motos.Application.Services;
using Motos.Domain.Entities;
using Motos.Domain.Interfaces;
using Motos.Tests.Mocks;

namespace Motos.Tests.UnitTest.Application
{
    public class MotoServiceTests
    {
        private readonly Mock<IMotoRepository> _repositoryMock;
        private readonly MotoService _motoService;

        public MotoServiceTests()
        {
            _repositoryMock = new Mock<IMotoRepository>();
            _motoService = new MotoService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Deve cadastrar moto gerando novo Id único")]
        public void CadastrarMoto_DeveGerarIdUnico()
        {
            var moto = MotoMock.CriarMotoSemId();

            _repositoryMock.Setup(r => r.ExisteMotoPorId(It.IsAny<string>()))
                           .Returns(false);

            _repositoryMock.Setup(r => r.CadastrarMoto(It.IsAny<Moto>()))
                           .Returns<Moto>(m => m);

            var result = _motoService.CadastrarMoto(moto);

            Assert.NotNull(result.IdMoto);
            Assert.False(string.IsNullOrWhiteSpace(result.IdMoto));
            _repositoryMock.Verify(r => r.CadastrarMoto(It.IsAny<Moto>()), Times.Once);
        }

        [Fact(DisplayName = "Deve tentar novo Id quando Id gerado já existir")]
        public void CadastrarMoto_DeveRepetirGeracaoDeId_SeIdJaExistir()
        {
            var moto = MotoMock.CriarMotoSemId();
            bool primeiraVez = true;

            _repositoryMock.Setup(r => r.ExisteMotoPorId(It.IsAny<string>()))
                .Returns(() =>
                {
                    if (primeiraVez)
                    {
                        primeiraVez = false;
                        return true; 
                    }
                    return false; 
                });

            _repositoryMock.Setup(r => r.CadastrarMoto(It.IsAny<Moto>()))
                .Returns<Moto>(m => m);

            var result = _motoService.CadastrarMoto(moto);

            Assert.NotNull(result.IdMoto);
            _repositoryMock.Verify(r => r.ExisteMotoPorId(It.IsAny<string>()), Times.AtLeast(2));
            _repositoryMock.Verify(r => r.CadastrarMoto(It.IsAny<Moto>()), Times.Once);
        }

        [Fact(DisplayName = "Deve atualizar moto com sucesso")]
        public void AtualizarMoto_DeveChamarRepositorio()
        {
            var moto = MotoMock.CriarMotoPadrao();

            _repositoryMock.Setup(r => r.AtualizarMoto(moto)).Returns(moto);

            var result = _motoService.AtualizarMoto(moto);

            Assert.Equal(moto, result);
            _repositoryMock.Verify(r => r.AtualizarMoto(moto), Times.Once);
        }

        [Fact(DisplayName = "Deve deletar moto por Id")]
        public void DeletarMotoPorId_DeveRetornarMotoDeletada()
        {
            var moto = MotoMock.CriarMotoPadrao();

            _repositoryMock.Setup(r => r.DeletarMotoPorId(moto.IdMoto)).Returns(moto);

            var result = _motoService.DeletarMotoPorId(moto.IdMoto);

            Assert.Equal(moto, result);
            _repositoryMock.Verify(r => r.DeletarMotoPorId(moto.IdMoto), Times.Once);
        }

        [Fact(DisplayName = "Deve obter moto por Id")]
        public void ObterMotoPorId_DeveRetornarMoto()
        {
            var moto = MotoMock.CriarMotoPadrao();

            _repositoryMock.Setup(r => r.ObterMotoPorId(moto.IdMoto)).Returns(moto);

            var result = _motoService.ObterMotoPorId(moto.IdMoto);

            Assert.Equal(moto, result);
            _repositoryMock.Verify(r => r.ObterMotoPorId(moto.IdMoto), Times.Once);
        }

        [Fact(DisplayName = "Deve obter todas as motos de um pátio")]
        public void ObterTodasAsMotosDoPatio_DeveRetornarLista()
        {
            var motos = MotoMock.CriarListaMotos();

            _repositoryMock.Setup(r => r.ObterTodasAsMotosDoPatio("p1"))
                .Returns(motos);

            var result = _motoService.ObterTodasAsMotosDoPatio("p1");

            var lista = Assert.IsAssignableFrom<IEnumerable<Moto>>(result);
            Assert.Equal(2, ((List<Moto>)lista).Count);
            _repositoryMock.Verify(r => r.ObterTodasAsMotosDoPatio("p1"), Times.Once);
        }
    }
}
