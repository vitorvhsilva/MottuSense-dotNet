using Motos.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entities
{
    [Table("TB_EVENTO_MOTO")]
    public class EventoMoto
    {
        [Key]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdEventoMoto { get; set; }
        [ForeignKey("Moto")]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdMoto { get; set; }
        [ForeignKey("Evento")]
        [Column(TypeName = "NUMBER")]
        public int IdEvento { get; set; }
        [Column(TypeName = "NUMBER(1)")]
        public bool EventoVisualizado { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime DataHoraEvento { get; set; }

        //navegacao
        public virtual Moto Moto { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
