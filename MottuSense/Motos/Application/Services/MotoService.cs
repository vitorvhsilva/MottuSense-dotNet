using Motos.Application.Interfaces;
using Motos.Domain.Entitites;
using Motos.Domain.Interfaces;
using Motos.Presentation.Dto.Moto;

namespace Motos.Application.Services
{
    public class MotoService: IMotoService
    {
        private readonly IMotoRepository _repository;

        public MotoService(IMotoRepository motoRepository)
        {
            _repository = motoRepository;
        }

        public Moto AtualizarMoto(AtualizarMotoInputDTO dto)
        {
            throw new NotImplementedException();
        }

        public Moto CadastrarMoto(CadastrarMotoInputDTO dto)
        {
            throw new NotImplementedException();
        }

        public Moto ObterMotoPorId(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Moto> ObterTodasAsMotosDoPatio(string id)
        {
            throw new NotImplementedException();
        }
    }
}
