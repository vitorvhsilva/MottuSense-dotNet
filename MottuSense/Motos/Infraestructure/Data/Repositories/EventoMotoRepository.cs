using Motos.Domain.Entities;
using Motos.Domain.Interfaces;
using Motos.Infraestructure.Data.AppData;

namespace Motos.Infraestructure.Data.Repositories
{
    public class EventoMotoRepository : IEventoMotoRepository
    {
        private readonly ApplicationContext _context;

        public EventoMotoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public EventoMoto PegarEventoPorIdEventoMoto(string IdEventoMoto)
        {
            return _context.EventoMoto.FirstOrDefault(e => e.IdEventoMoto == IdEventoMoto);
        }

        public IEnumerable<EventoMoto> PegarEventosPorIdMoto(string IdMoto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventoMoto> PegarEventosPorIdPatio(string IdMoto)
        {
            throw new NotImplementedException();
        }

        public EventoMoto PublicarEvento(EventoMoto evento)
        {
            _context.EventoMoto.Add(evento);
            _context.SaveChanges();

            return evento;
        }

        public void VisualizarEvento(string id)
        {
            var evento = _context.EventoMoto.Find(id);
            evento.EventoVisualizado = true;
            _context.SaveChanges();
        }

        public bool ExisteEventoPorIdEvento(string IdEventoMoto)
        {
            try
            {
                return _context.EventoMoto.Where(e => e.IdEventoMoto == IdEventoMoto).Any();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro na aplicação: {e.Message}");
                return false;
            }
        }
    }
}
