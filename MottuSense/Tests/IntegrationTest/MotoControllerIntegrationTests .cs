using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.Localizacao;
using Motos.Presentation.Dto.Moto;
using Motos.Tests.Mocks;
using System.Net;
using System.Net.Http.Json;
using Tests.Mocks;
using Xunit;

namespace Motos.Tests.IntegrationTest
{

    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IMotoService> MotoServiceMock { get; } = new();
        public Mock<ILocalizacaoService> LocalizacaoServiceMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IMotoService));
                services.AddSingleton(MotoServiceMock.Object);

                services.RemoveAll(typeof(ILocalizacaoService));
                services.AddSingleton(LocalizacaoServiceMock.Object);
            });
        }
    }

    public class MotoControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private const string ApiKey = "MOTTUSENSEAPIKEY";

        public MotoControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        private HttpClient CreateClientWithApiKey()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            return client;
        }

        [Fact]
        public async Task ObterTodasAsMotosDoPatio_RetornaOk()
        {
            // Arrange
            var patioId = "patio-01";
            var motos = MotoMock.CriarListaMotos();
            _factory.MotoServiceMock.Setup(s => s.ObterTodasAsMotosDoPatio(patioId)).Returns(motos);

            var client = CreateClientWithApiKey();

            // Act
            var response = await client.GetAsync($"/api/v1/motos/patios/{patioId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ObterMotoPorId_RetornaOk()
        {
            // Arrange
            var motoDto = MotoControllerMock.ObterMotoOutputPadrao();
            var motoEntity = MotoMock.CriarMotoPadrao();
            motoEntity.IdMoto = motoDto.IdMoto;

            _factory.MotoServiceMock.Setup(s => s.ObterMotoPorId(motoDto.IdMoto)).Returns(motoEntity);
            _factory.LocalizacaoServiceMock.Setup(s => s.ObterLocalizacaoPeloId(motoDto.IdMoto))
                .Returns(LocalizacaoMotoMock.Criar());

            var client = CreateClientWithApiKey();

            // Act
            var response = await client.GetAsync($"/api/v1/motos/{motoDto.IdMoto}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CadastrarMoto_RetornaCreated()
        {
            // Arrange
            var input = MotoControllerMock.CadastrarMotoInputPadrao();
            var output = MotoControllerMock.CadastrarMotoOutputPadrao();

            _factory.MotoServiceMock.Setup(s => s.CadastrarMoto(It.IsAny<Moto>()))
                .Returns(MotoMock.CriarMotoPadrao());

            _factory.LocalizacaoServiceMock.Setup(s => s.CadastrarLocalizacaoDaMoto(It.IsAny<string>()))
                .Returns(LocalizacaoMotoMock.Criar());

            var client = CreateClientWithApiKey();

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/motos", input);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task AtualizarMoto_RetornaOk()
        {
            // Arrange
            var input = MotoControllerMock.AtualizarMotoInputPadrao();
            var motoAtualizada = MotoMock.CriarMotoPadrao();
            motoAtualizada.IdMoto = input.IdMoto;

            _factory.MotoServiceMock.Setup(s => s.AtualizarMoto(It.IsAny<Moto>())).Returns(motoAtualizada);

            var client = CreateClientWithApiKey();

            // Act
            var response = await client.PutAsJsonAsync("/api/v1/motos", input);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeletarMotoPorId_RetornaNoContent()
        {
            // Arrange
            var moto = MotoMock.CriarMotoPadrao();

            _factory.MotoServiceMock.Setup(s => s.DeletarMotoPorId(moto.IdMoto)).Returns(moto);

            var client = CreateClientWithApiKey();

            // Act
            var response = await client.DeleteAsync($"/api/v1/motos/{moto.IdMoto}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
