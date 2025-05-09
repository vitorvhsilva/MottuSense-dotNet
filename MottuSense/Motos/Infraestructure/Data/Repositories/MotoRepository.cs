using Motos.Domain.Entitites;
using Motos.Domain.Interfaces;
using Motos.Infraestructure.Data.AppData;
using System.Numerics;

namespace Motos.Infraestructure.Data.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationContext _context;
        public MotoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Moto AtualizarMoto(Moto moto)
        {
            Moto entityMoto = _context.Moto.FirstOrDefault(m => m.IdMoto == moto.IdMoto);

            if (entityMoto is null)
                return null;

            entityMoto.PlacaMoto = moto.PlacaMoto;
            entityMoto.IotMoto = moto.IotMoto;  
            entityMoto.ChassiMoto = moto.ChassiMoto;    
            entityMoto.StatusMoto = moto.StatusMoto;        
            entityMoto.ModeloMoto = moto.ModeloMoto;

            _context.SaveChanges();

            return entityMoto;

        }

        public Moto CadastrarMoto(Moto moto)
        {
            _context.Moto.Add(moto);
            _context.SaveChanges();

            return ObterMotoPorPlaca(moto.PlacaMoto);
        }

        public Moto ObterMotoPorId(string id)
        {
            return _context.Moto.FirstOrDefault(m => m.IdMoto == id);
        }

        public Moto ObterMotoPorPlaca(string placa)
        {
            return _context.Moto.FirstOrDefault(m => m.PlacaMoto == placa);
        }

        public bool ExisteMotoPorId(string id)
        {
            try
            {
                return _context.Moto.Where(m => m.IdMoto == id).Any();
            } catch(Exception e)
            {
                Console.WriteLine($"Erro na aplicação: {e.Message}");
                return false;
            }
        }


        public IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id)
        {
            return _context.Moto.Where(m => m.IdPatio == id).ToList();
        }

        public Moto DeletarMotoPorId(string IdMoto)
        {
            var moto = _context.Moto.FirstOrDefault(m => m.IdMoto == IdMoto);

            if (moto is null)
            {
                return null;
            }

            _context.Moto.Remove(moto);
            _context.SaveChanges();

            return moto;
        }
    }
}
