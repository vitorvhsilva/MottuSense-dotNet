using Motos.Domain.Entities.Enums;
using Motos.Presentation.Dto.Moto;
using Motos.Presentation.Dto.Localizacao;
using Motos.Presentation.Dto.Output;
using System.Collections.Generic;

namespace Tests.Mocks
{
    public static class MotoControllerMock
    {
        public static ObterMotosOutputDTO ObterMotosOutputPadrao()
        {
            return new ObterMotosOutputDTO(
                IdMoto: "moto-001",
                PlacaMoto: "ABC123",
                ModeloMoto: ModeloMoto.MOTTU_E,
                StatusMoto: StatusMoto.PRONTA_PARA_ALUGUEL,
                IdPatio: "patio-01"
            );
        }

        public static List<ObterMotosOutputDTO> ObterListaMotosOutput()
        {
            return new List<ObterMotosOutputDTO>
            {
                ObterMotosOutputPadrao(),
                new ObterMotosOutputDTO("moto-002", "XYZ987", ModeloMoto.MOTTU_E, StatusMoto.AGENDADA_PARA_MANUTENCAO, "patio-02")
            };
        }

        public static ObterMotoOutputDTO ObterMotoOutputPadrao()
        {
            var localizacao = new LocalizacaoDTO("moto-001", "10.0", "20.0");
            return new ObterMotoOutputDTO(
                IdMoto: "moto-001",
                PlacaMoto: "ABC123",
                ModeloMoto: ModeloMoto.MOTTU_E,
                StatusMoto: StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto: "CHS123456",
                IotMoto: "iot-123",
                IdPatio: "patio-01",
                Localizacao: localizacao
            );
        }

        public static List<ObterMotoOutputDTO> ObterListaMotoOutput()
        {
            return new List<ObterMotoOutputDTO>
            {
                ObterMotoOutputPadrao(),
                new ObterMotoOutputDTO(
                    "moto-002",
                    "XYZ987",
                    ModeloMoto.MOTTU_E,
                    StatusMoto.AGENDADA_PARA_MANUTENCAO,
                    "CHS654321",
                    "iot-456",
                    "patio-02",
                    new LocalizacaoDTO("moto-002", "11.0", "21.0")
                )
            };
        }

        public static CadastrarMotoInputDTO CadastrarMotoInputPadrao()
        {
            return new CadastrarMotoInputDTO(
                PlacaMoto: "ABC123",
                ModeloMoto: ModeloMoto.MOTTU_E,
                StatusMoto: StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto: "CHS123456",
                IotMoto: "iot-123",
                IdPatio: "patio-01"
            );
        }

        public static CadastrarMotoOutputDTO CadastrarMotoOutputPadrao()
        {
            return new CadastrarMotoOutputDTO(
                IdMoto: "moto-001",
                PlacaMoto: "ABC123",
                ModeloMoto: ModeloMoto.MOTTU_E,
                StatusMoto: StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto: "CHS123456",
                IotMoto: "iot-123",
                IdPatio: "patio-01"
            );
        }

        public static AtualizarMotoInputDTO AtualizarMotoInputPadrao()
        {
            return new AtualizarMotoInputDTO(
                IdMoto: "moto-001",
                PlacaMoto: "ABC123",
                ModeloMoto: ModeloMoto.MOTTU_E,
                StatusMoto: StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto: "CHS123456",
                IotMoto: "iot-123",
                IdPatio: "patio-01"
            );
        }

        public static AtualizarMotoOutputDTO AtualizarMotoOutputPadrao()
        {
            return new AtualizarMotoOutputDTO(
                IdMoto: "moto-001",
                PlacaMoto: "ABC123",
                ModeloMoto: ModeloMoto.MOTTU_E,
                StatusMoto: StatusMoto.PRONTA_PARA_ALUGUEL,
                ChassiMoto: "CHS123456",
                IotMoto: "iot-123",
                IdPatio: "patio-01"
            );
        }

        public static LocalizacaoDTO ObterLocalizacaoPadrao()
        {
            return new LocalizacaoDTO("moto-001", "10.0", "20.0");
        }
    }
}
