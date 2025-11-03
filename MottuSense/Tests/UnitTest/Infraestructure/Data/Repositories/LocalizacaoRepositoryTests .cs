using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using Motos.Domain.Entities;
using Motos.Infraestructure.Data.AppData;
using Motos.Infraestructure.Data.Repositories;
using Motos.Tests.Mocks;

namespace Motos.Tests.UnitTest.Infrastructure.Data.Repositories
{
    public class LocalizacaoRepositoryTests : IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly LocalizacaoRepository _repo;

        public LocalizacaoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"db_localizacao_{Guid.NewGuid():N}")
                .Options;

            _context = new ApplicationContext(options);
            _repo = new LocalizacaoRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact(DisplayName = "CadastrarLocalizacaoDaMoto deve adicionar localização")]
        public void CadastrarLocalizacaoDaMoto_DeveAdicionar()
        {
            var localizacao = LocalizacaoMotoMock.Criar(latitude: "10.0", longitude: "20.0");

            var result = _repo.CadastrarLocalizacaoDaMoto(localizacao);

            Assert.NotNull(result);
            Assert.Equal("10.0", result.LatitudeMoto);
            Assert.Equal("20.0", result.LongitudeMoto);
            Assert.Single(_context.LocalizacaoMoto);
        }

        [Fact(DisplayName = "ObterLocalizacaoPeloId deve retornar localização existente")]
        public void ObterLocalizacaoPeloId_DeveRetornarLocalizacao()
        {
            var localizacao = LocalizacaoMotoMock.Criar();
            _context.LocalizacaoMoto.Add(localizacao);
            _context.SaveChanges();

            var result = _repo.ObterLocalizacaoPeloId(localizacao.IdMoto);

            Assert.NotNull(result);
            Assert.Equal(localizacao.IdMoto, result.IdMoto);
            Assert.Equal(localizacao.LatitudeMoto, result.LatitudeMoto);
            Assert.Equal(localizacao.LongitudeMoto, result.LongitudeMoto);
        }
    }
}
