using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Tests.Mocks;
using System.Net;
using System.Net.Http.Json;
using Tests.Mocks;
using Xunit;

namespace Motos.Tests.IntegrationTest
{
    public class CustomWebApplicationFactoryEvento : WebApplicationFactory<Program>
    {
        public Mock<IEventoMotoService> EventoMotoServiceMock { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IEventoMotoService));
                services.AddSingleton(EventoMotoServiceMock.Object);
            });
        }
    }

    public class EventoMotoControllerIntegrationTests : IClassFixture<CustomWebApplicationFactoryEvento>
    {
        private readonly CustomWebApplicationFactoryEvento _factory;
        private const string ApiKey = "MOTTUSENSEAPIKEY";

        public EventoMotoControllerIntegrationTests(CustomWebApplicationFactoryEvento factory)
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
        public async Task PegarEventoPorIdEventoMoto_RetornaOk()
        {
            var evento = EventoMotoControllerMock.ObterEventoOutputPadrao();
            _factory.EventoMotoServiceMock.Setup(s => s.PegarEventoPorIdEventoMoto(evento.IdEventoMoto))
                .Returns(EventoMotoMock.CriarEventoMotoPadrao());

            var client = CreateClientWithApiKey();
            var response = await client.GetAsync($"/api/v1/eventos/{evento.IdEventoMoto}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PegarEventoPorIdEventoMoto_RetornaBadRequest()
        {
            _factory.EventoMotoServiceMock.Setup(s => s.PegarEventoPorIdEventoMoto("invalido"))
                .Returns((EventoMoto?)null);

            var client = CreateClientWithApiKey();
            var response = await client.GetAsync("/api/v1/eventos/invalido");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PegarEventosPorIdMoto_RetornaOk()
        {
            var eventos = EventoMotoControllerMock.ObterListaEventosOutput();
            _factory.EventoMotoServiceMock.Setup(s => s.PegarEventosPorIdMoto("moto-01"))
                .Returns(EventoMotoMock.CriarListaEventos());

            var client = CreateClientWithApiKey();
            var response = await client.GetAsync("/api/v1/eventos/motos/moto-01");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PegarEventosPorIdPatio_RetornaOk()
        {
            var eventos = EventoMotoControllerMock.ObterListaEventosOutput();
            _factory.EventoMotoServiceMock.Setup(s => s.PegarEventosPorIdPatio("patio-01"))
                .Returns(EventoMotoMock.CriarListaEventos());

            var client = CreateClientWithApiKey();
            var response = await client.GetAsync("/api/v1/eventos/patios/patio-01");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_CadastrarEvento_RetornaCreated()
        {
            var input = EventoMotoControllerMock.CriarEventoInputPadrao();
            var eventoPublicado = EventoMotoMock.CriarEventoMotoPadrao();

            _factory.EventoMotoServiceMock.Setup(s => s.PublicarEvento(It.IsAny<EventoMoto>()))
                .Returns(eventoPublicado);

            var client = CreateClientWithApiKey();
            var response = await client.PostAsJsonAsync("/api/v1/eventos", input);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task VisualizarEventos_RetornaOk()
        {
            var input = EventoMotoControllerMock.CriarVisualizarEventosDTOPadrao();

            var client = CreateClientWithApiKey();
            var response = await client.PatchAsJsonAsync("/api/v1/eventos/visualizar", input);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
