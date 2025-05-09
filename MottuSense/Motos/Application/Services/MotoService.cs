using Motos.Application.Interfaces;
using Motos.Domain.Entitites;
using Motos.Domain.Interfaces;

namespace Motos.Application.Services
{
    public class MotoService: IMotoService
    {
        private readonly IMotoRepository _repository;

        public MotoService(IMotoRepository motoRepository)
        {
            _repository = motoRepository;
        }

        public Moto AtualizarMoto(Moto moto)
        {
            throw new NotImplementedException();
        }

        public Moto CadastrarMoto(Moto moto)
        {
            string IdMoto = null;
            do
            {
                IdMoto = Guid.NewGuid().ToString();
            } while (_repository.ExisteMotoPorId(IdMoto));

            moto.IdMoto = IdMoto;
            Moto entityMoto = _repository.CadastrarMoto(moto);
            return entityMoto;
        }

        public Moto DeletarMotoPorId(string IdMoto)
        {
            return _repository.DeletarMotoPorId(IdMoto);
        }

        public Moto ObterMotoPorId(string id)
        {
            return _repository.ObterMotoPorId(id);
        }

        public IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id)
        {
            return _repository.ObterTodasAsMotosDoPatio(id);
        }
    }
}
