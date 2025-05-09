using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entities
{
    [Table("TB_LOCALIZACAO_MOTO")]
    public class LocalizacaoMoto
    {
        [Key]
        [ForeignKey("Moto")]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdMoto { get; set; }
        [Column(TypeName = "VARCHAR2(500)")]
        public string? LatitudeMoto { get; set; }
        [Column(TypeName = "VARCHAR2(500)")]
        public string? LongitudeMoto { get; set; }

        // navegacao
        public Moto? Moto { get; set; } 
    }
}
