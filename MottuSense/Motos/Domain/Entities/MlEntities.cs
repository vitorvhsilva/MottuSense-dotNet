using Microsoft.ML.Data;

namespace Motos.Domain.Entities
{
    public class MotoML
    {
        public string ModeloMoto { get; set; } = string.Empty;
        public string StatusMoto { get; set; } = string.Empty;
        public string IdPatio { get; set; } = string.Empty;
        public float QtdEventosRecentes { get; set; }
        public bool Previsto { get; set; } // Label
    }

    public class PreverManutencaoOutput
    {
        [ColumnName("PredictedLabel")]
        public bool Previsto { get; set; }

        public float Probability { get; set; }
    }
}
