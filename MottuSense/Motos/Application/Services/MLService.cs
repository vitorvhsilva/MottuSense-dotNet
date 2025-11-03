using Microsoft.ML;
using Motos.Application.Interfaces;
using Motos.Domain.Entities;
using Motos.Presentation.Dto.Ml;

namespace Motos.Application.Services
{
    public class MLService: IMLService
    {
        private readonly MLContext _mlContext;
        private ITransformer _modelo;
        private PredictionEngine<MotoML, PreverManutencaoOutput> _predictionEngine;

        public MLService()
        {
            _mlContext = new MLContext(seed: 0);
            TreinarModelo();
        }

        private void TreinarModelo()
        {
            var dados = new List<MotoML>
            {
                new MotoML { ModeloMoto="MOTTU_E", StatusMoto="PRONTA_PARA_ALUGUEL", IdPatio="patio-01", QtdEventosRecentes=0, Previsto=false },
                new MotoML { ModeloMoto="MOTTU_E", StatusMoto="AGENDADA_PARA_MANUTENCAO", IdPatio="patio-01", QtdEventosRecentes=2, Previsto=true },
                new MotoML { ModeloMoto="MOTTU_X", StatusMoto="PRONTA_PARA_ALUGUEL", IdPatio="patio-02", QtdEventosRecentes=1, Previsto=false },
                new MotoML { ModeloMoto="MOTTU_X", StatusMoto="AGENDADA_PARA_MANUTENCAO", IdPatio="patio-02", QtdEventosRecentes=3, Previsto=true },
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(dados);

            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding("ModeloMoto")
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding("StatusMoto"))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding("IdPatio"))
                .Append(_mlContext.Transforms.Concatenate("Features", "ModeloMoto", "StatusMoto", "IdPatio", "QtdEventosRecentes"))
                .Append(_mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Previsto"));

            _modelo = pipeline.Fit(dataView);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<MotoML, PreverManutencaoOutput>(_modelo);
        }

        public PreverManutencaoOutputDTO Prever(PreverManutencaoInputDTO dto)
        {
            var input = new MotoML
            {
                ModeloMoto = dto.ModeloMoto,
                StatusMoto = dto.StatusMoto,
                IdPatio = dto.IdPatio,
                QtdEventosRecentes = dto.QtdEventosRecentes
            };

            var resultado = _predictionEngine.Predict(input);

            return new PreverManutencaoOutputDTO(
                dto.IdMoto,
                resultado.Previsto,
                resultado.Probability
            );
        }
    }
}
