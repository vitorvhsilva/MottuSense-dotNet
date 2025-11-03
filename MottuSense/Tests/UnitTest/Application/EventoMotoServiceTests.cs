using Moq;
using Motos.Application.Services;
using Motos.Domain.Entities;
using Motos.Domain.Interfaces;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Tests.Mocks;

namespace Motos.Tests.UnitTest.Application
{
    public class EventoMotoServiceTests
    {
        private readonly Mock<IEventoMotoRepository> _repositoryMock;
        private readonly EventoMotoService _service;

        public EventoMotoServiceTests()
        {
            _repositoryMock = new Mock<IEventoMotoRepository>();
            _service = new EventoMotoService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Deve publicar evento gerando novo Id único")]
        public void PublicarEvento_DeveGerarIdUnico()
        {
            var evento = EventoMotoMock.CriarEventoMotoSemId();
            _repositoryMock.Setup(r => r.ExisteEventoPorIdEvento(It.IsAny<string>())).Returns(false);
            _repositoryMock.Setup(r => r.PublicarEvento(It.IsAny<EventoMoto>()))
                           .Returns<EventoMoto>(e => e);

            var result = _service.PublicarEvento(evento);

            Assert.NotNull(result.IdEventoMoto);
            Assert.False(result.EventoVisualizado);
            _repositoryMock.Verify(r => r.PublicarEvento(It.IsAny<EventoMoto>()), Times.Once);
        }

        [Fact(DisplayName = "Deve marcar eventos como visualizado")]
        public void MarcarEventosComoVisualizado_DeveChamarRepositorio()
        {
            var dto = new VisualizarEventosDTO(new List<string> { "11", "2", "3" });
            // Act
            _service.MarcarEventosComoVisualizado(dto);

            foreach (var id in dto.IdEventos)
            {
                _repositoryMock.Verify(r => r.VisualizarEvento(id), Times.Once);
            }
        }

        [Fact(DisplayName = "Deve obter evento por IdEventoMoto")]
        public void PegarEventoPorIdEventoMoto_DeveRetornarEvento()
        {
            var evento = EventoMotoMock.CriarEventoMotoPadrao();
            _repositoryMock.Setup(r => r.PegarEventoPorIdEventoMoto(evento.IdEventoMoto))
                           .Returns(evento);

            var result = _service.PegarEventoPorIdEventoMoto(evento.IdEventoMoto);

            Assert.Equal(evento, result);
            _repositoryMock.Verify(r => r.PegarEventoPorIdEventoMoto(evento.IdEventoMoto), Times.Once);
        }

        [Fact(DisplayName = "Deve obter eventos por IdMoto")]
        public void PegarEventosPorIdMoto_DeveRetornarLista()
        {
            var eventos = EventoMotoMock.CriarListaEventos();
            _repositoryMock.Setup(r => r.PegarEventosPorIdMoto("moto-001"))
                           .Returns(eventos);

            var result = _service.PegarEventosPorIdMoto("moto-001");

            Assert.Equal(2, ((List<EventoMoto>)result).Count);
            _repositoryMock.Verify(r => r.PegarEventosPorIdMoto("moto-001"), Times.Once);
        }

        [Fact(DisplayName = "Deve obter eventos por IdPatio")]
        public void PegarEventosPorIdPatio_DeveRetornarLista()
        {
            var eventos = EventoMotoMock.CriarListaEventos();
            _repositoryMock.Setup(r => r.PegarEventosPorIdPatio("patio-01"))
                           .Returns(eventos);

            var result = _service.PegarEventosPorIdPatio("patio-01");

            Assert.Equal(2, ((List<EventoMoto>)result).Count);
            _repositoryMock.Verify(r => r.PegarEventosPorIdPatio("patio-01"), Times.Once);
        }
    }
}
