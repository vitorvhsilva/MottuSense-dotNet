using Motos.Application.Interfaces;
using Motos.Domain.Interfaces;

namespace Motos.Application.Services
{
    public class MotoService: IMotoService
    {
        private readonly IMotoRepository _repository;
    }
}
