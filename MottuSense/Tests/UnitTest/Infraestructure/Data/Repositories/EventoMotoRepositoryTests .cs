using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Motos.Domain.Entities;
using Motos.Infraestructure.Data.AppData;
using Motos.Infraestructure.Data.Repositories;
using Motos.Tests.Mocks;

namespace Motos.Tests.Infrastructure
{
    public class EventoMotoRepositoryTests : IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly EventoMotoRepository _repo;

        public EventoMotoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"db_eventos_{Guid.NewGuid():N}")
                .Options;

            _context = new ApplicationContext(options);
            _repo = new EventoMotoRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact(DisplayName = "PublicarEvento deve adicionar evento e retornar entidade")]
        public void PublicarEvento_DeveAdicionarEvento()
        {
            var evento = EventoMotoMock.CriarEventoMotoPadrao();

            var result = _repo.PublicarEvento(evento);

            Assert.NotNull(result);
            Assert.False(result.EventoVisualizado);
            Assert.Single(_context.EventoMoto);
        }

        [Fact(DisplayName = "PegarEventoPorIdEventoMoto deve retornar evento correto")]
        public void PegarEventoPorIdEventoMoto_DeveRetornarEvento()
        {
            var evento = EventoMotoMock.CriarEventoMotoPadrao();
            _context.EventoMoto.Add(evento);
            _context.SaveChanges();

            var result = _repo.PegarEventoPorIdEventoMoto(evento.IdEventoMoto);

            Assert.NotNull(result);
            Assert.Equal(evento.IdEventoMoto, result.IdEventoMoto);
        }

        [Fact(DisplayName = "PegarEventosPorIdMoto deve retornar lista de eventos")]
        public void PegarEventosPorIdMoto_DeveRetornarLista()
        {
            var eventos = EventoMotoMock.CriarListaEventos();
            _context.EventoMoto.AddRange(eventos);
            _context.SaveChanges();

            var result = _repo.PegarEventosPorIdMoto("moto-001").ToList();

            Assert.Equal(2, result.Count);
        }

        [Fact(DisplayName = "VisualizarEvento deve marcar evento como visualizado")]
        public void VisualizarEvento_DeveMarcarEvento()
        {
            var evento = EventoMotoMock.CriarEventoMotoPadrao();
            _context.EventoMoto.Add(evento);
            _context.SaveChanges();

            _repo.VisualizarEvento(evento.IdEventoMoto);

            var atualizado = _context.EventoMoto.Find(evento.IdEventoMoto);
            Assert.True(atualizado.EventoVisualizado);
        }

        [Fact(DisplayName = "ExisteEventoPorId deve retornar true se existir")]
        public void ExisteEventoPorId_DeveRetornarTrue()
        {
            var evento = EventoMotoMock.CriarEventoMotoPadrao();
            _context.EventoMoto.Add(evento);
            _context.SaveChanges();

            var existe = _repo.ExisteEventoPorIdEvento(evento.IdEventoMoto);

            Assert.True(existe);
        }

        [Fact(DisplayName = "ExisteEventoPorId deve retornar false se não existir")]
        public void ExisteEventoPorId_DeveRetornarFalse()
        {
            var existe = _repo.ExisteEventoPorIdEvento("ID_INEXISTENTE");

            Assert.False(existe);
        }
    }
}
