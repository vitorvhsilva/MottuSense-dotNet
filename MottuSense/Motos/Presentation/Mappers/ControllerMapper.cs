using AutoMapper;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.EventoMoto;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Dto.Output;

namespace Motos.Presentation.Mappers
{
    public class ControllerMapper: Profile
    {
        public ControllerMapper()
        {
            CreateMap<CadastrarEventoMotoInputDTO, EventoMoto>();
            CreateMap<EventoMoto, CadastrarEventoMotoOutputDTO>();
            CreateMap<EventoMoto, ObterEventoMotoDTO>();

            CreateMap<CadastrarMotoInputDTO, Moto>();
            CreateMap<Moto, CadastrarMotoOutputDTO>();

            CreateMap<Moto, ObterMotoOutputDTO>();
            CreateMap<Moto, ObterMotosOutputDTO>();

            CreateMap<AtualizarMotoInputDTO, Moto>();
            CreateMap<Moto, AtualizarMotoOutputDTO>();
        }
    }
}
