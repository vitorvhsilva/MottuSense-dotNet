using AutoMapper;
using Motos.Domain.Entitites;
using Motos.Presentation.Dto.Moto;

namespace Motos.Presentation.Mappers
{
    public class MotoControllerMapper: Profile
    {
        public MotoControllerMapper()
        {
            CreateMap<CadastrarMotoInputDTO, Moto>();
            CreateMap<Moto, CadastrarMotoOutputDTO>();
        }
    }
}
