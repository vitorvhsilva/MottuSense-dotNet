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
    public class MotoRepositoryTests : IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly MotoRepository _repo;

        public MotoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"db_motos_{Guid.NewGuid():N}")
                .Options;

            _context = new ApplicationContext(options);
            _repo = new MotoRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact(DisplayName = "CadastrarMoto deve adicionar moto e retornar entidade")]
        public void CadastrarMoto_DeveAdicionarMoto()
        {
            var moto = MotoMock.CriarMotoPadrao();

            var result = _repo.CadastrarMoto(moto);

            Assert.NotNull(result);
            Assert.Equal(moto.PlacaMoto, result.PlacaMoto);
            Assert.Single(_context.Moto);
        }

        [Fact(DisplayName = "ObterMotoPorId deve retornar moto existente")]
        public void ObterMotoPorId_DeveRetornarMoto()
        {
            var moto = MotoMock.CriarMotoPadrao();
            _context.Moto.Add(moto);
            _context.SaveChanges();

            var result = _repo.ObterMotoPorId(moto.IdMoto);

            Assert.NotNull(result);
            Assert.Equal(moto.IdMoto, result.IdMoto);
        }

        [Fact(DisplayName = "AtualizarMoto deve modificar dados existentes")]
        public void AtualizarMoto_DeveAtualizarMoto()
        {
            var moto = MotoMock.CriarMotoPadrao();
            _context.Moto.Add(moto);
            _context.SaveChanges();

            moto.IotMoto = "NOVO_IOT";
            moto.StatusMoto = Domain.Entities.Enums.StatusMoto.AGENDADA_PARA_MANUTENCAO;

            var result = _repo.AtualizarMoto(moto);

            Assert.Equal("NOVO_IOT", result.IotMoto);
            Assert.Equal(Domain.Entities.Enums.StatusMoto.AGENDADA_PARA_MANUTENCAO, result.StatusMoto);
        }

        [Fact(DisplayName = "DeletarMotoPorId deve remover moto do contexto")]
        public void DeletarMotoPorId_DeveRemoverMoto()
        {
            var moto = MotoMock.CriarMotoPadrao();
            _context.Moto.Add(moto);
            _context.SaveChanges();

            var result = _repo.DeletarMotoPorId(moto.IdMoto);

            Assert.Equal(moto.IdMoto, result.IdMoto);
            Assert.Empty(_context.Moto);
        }

        [Fact(DisplayName = "ObterTodasAsMotosDoPatio deve retornar motos corretas")]
        public void ObterTodasAsMotosDoPatio_DeveRetornarLista()
        {
            var motos = MotoMock.CriarListaMotos();
            _context.Moto.AddRange(motos);
            _context.SaveChanges();

            var result = _repo.ObterTodasAsMotosDoPatio("p1").ToList();

            Assert.Equal(2, result.Count);
            Assert.All(result, m => Assert.Equal("p1", m.IdPatio));
        }

        [Fact(DisplayName = "ExisteMotoPorId deve retornar true se existir")]
        public void ExisteMotoPorId_DeveRetornarTrue()
        {
            var moto = MotoMock.CriarMotoPadrao();
            _context.Moto.Add(moto);
            _context.SaveChanges();

            var existe = _repo.ExisteMotoPorId(moto.IdMoto);

            Assert.True(existe);
        }

        [Fact(DisplayName = "ExisteMotoPorId deve retornar false se não existir")]
        public void ExisteMotoPorId_DeveRetornarFalse()
        {
            var existe = _repo.ExisteMotoPorId("ID_INEXISTENTE");

            Assert.False(existe);
        }
    }
}
