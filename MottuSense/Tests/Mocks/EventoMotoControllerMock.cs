using Motos.Presentation.Dto.EventoMoto;

namespace Tests.Mocks
{
    public static class EventoMotoControllerMock
    {
        public static CadastrarEventoMotoInputDTO CriarEventoInputPadrao()
        {
            return new CadastrarEventoMotoInputDTO(
                IdMoto: "moto-001",
                IdEvento: 1,
                DataHoraEvento: DateTime.UtcNow
            );
        }

        public static ObterEventoMotoDTO ObterEventoOutputPadrao()
        {
            return new ObterEventoMotoDTO(
                IdEventoMoto: "evento-001",
                IdMoto: "moto-001",
                IdEvento: 1,
                EventoVisualizado: false,
                DataHoraEvento: DateTime.UtcNow
            );
        }

        public static List<ObterEventoMotoDTO> ObterListaEventosOutput()
        {
            return new List<ObterEventoMotoDTO>
            {
                ObterEventoOutputPadrao(),
                new ObterEventoMotoDTO(
                    IdEventoMoto: "evento-002",
                    IdMoto: "moto-002",
                    IdEvento: 2,
                    EventoVisualizado: true,
                    DataHoraEvento: DateTime.UtcNow.AddMinutes(-10)
                )
            };
        }

        public static VisualizarEventosDTO CriarVisualizarEventosDTOPadrao()
        {
            return new VisualizarEventosDTO(
                IdEventos: new List<string> { "evento-001", "evento-002" }
            );
        }
    }
}
