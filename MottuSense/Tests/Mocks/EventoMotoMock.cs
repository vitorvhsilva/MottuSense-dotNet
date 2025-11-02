using Motos.Domain.Entities;

namespace Motos.Tests.Mocks
{
    public static class EventoMotoMock
    {
        public static EventoMoto CriarEventoMotoPadrao()
        {
            return new EventoMoto
            {
                IdEventoMoto = "evento-001",
                IdMoto = "moto-001",
                IdEvento = 1,
                EventoVisualizado = false,
                DataHoraEvento = DateTime.Now
            };
        }

        public static EventoMoto CriarEventoMotoSemId()
        {
            return new EventoMoto
            {
                IdMoto = "moto-002",
                IdEvento = 2,
                EventoVisualizado = false,
                DataHoraEvento = DateTime.Now
            };
        }

        public static List<EventoMoto> CriarListaEventos()
        {
            return new List<EventoMoto>
            {
                new EventoMoto { IdEventoMoto = "e1", IdMoto = "moto-001", IdEvento = 1, EventoVisualizado = false, DataHoraEvento = DateTime.Now },
                new EventoMoto { IdEventoMoto = "e2", IdMoto = "moto-001", IdEvento = 2, EventoVisualizado = true, DataHoraEvento = DateTime.Now }
            };
        }
    }
}
