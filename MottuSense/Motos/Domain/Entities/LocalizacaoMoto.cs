using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_LOCALIZACAO_MOTO")]
    public class LocalizacaoMoto
    {
        [Key]
        [ForeignKey("Moto")]
        public string IdMoto { get; set; }
        public string LatitudeMoto { get; set; }
        public string LongitudeMoto { get; set; }

        // navegacao
        public Moto Moto { get; set; } 
    }
}
