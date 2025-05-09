using Motos.Domain.Entities;
using Motos.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entities
{
    [Table("TB_MOTO")]
    public class Moto
    {
        [Key]
        [Column(TypeName = "VARCHAR2(255)")]
        public string? IdMoto { get; set; }
        [Column(TypeName = "VARCHAR2(50)")]
        public string PlacaMoto { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR2(100)")]
        public ModeloMoto ModeloMoto { get; set; }
        [Column(TypeName = "VARCHAR2(100)")]
        public StatusMoto StatusMoto { get; set; }
        [Column(TypeName = "VARCHAR2(500)")]
        public string ChassiMoto { get; set; } = string.Empty;
        [Column(TypeName = "VARCHAR2(500)")]
        public string IotMoto { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR2(255)")]
        public string IdPatio { get; set; } = string.Empty;

        //navegacao

        [ForeignKey(nameof(IdPatio))]
        public Patio? Patio { get; set; }
        public LocalizacaoMoto? LocalizacaoMoto { get; set; }
        public ICollection<EventoMoto>? EventosMoto { get; set; }
    }
}
