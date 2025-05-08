using AutoMapper;
using Motos.Domain.Entitites;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Dto.Output;

namespace Motos.Presentation.Mappers
{
    public class MotoControllerMapper: Profile
    {
        public MotoControllerMapper()
        {
            CreateMap<CadastrarMotoInputDTO, Moto>();
            CreateMap<Moto, CadastrarMotoOutputDTO>();
            CreateMap<Moto, ObterMotoOutputDTO>();
            CreateMap<Moto, ObterMotosOutputDTO>();
        }
    }
}
