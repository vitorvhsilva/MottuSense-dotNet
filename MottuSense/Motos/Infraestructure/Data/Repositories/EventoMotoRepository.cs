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

        public EventoMoto PegarEventoPorIdEvento(string IdEvento)
        {
            throw new NotImplementedException();
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

        public void VisualizarEvento(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
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
