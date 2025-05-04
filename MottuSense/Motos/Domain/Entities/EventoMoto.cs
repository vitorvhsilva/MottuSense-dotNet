using Motos.Domain.Entitites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entities
{
    [Table("TB_EVENTO_MOTO")]
    public class EventoMoto
    {
        [Key]
        public string IdEventoMoto { get; set; }
        [ForeignKey("Moto")]
        public string IdMoto { get; set; }
        [ForeignKey("Evento")]
        public string IdEvento { get; set; }
        public bool EventoVisualizado { get; set; }
        public DateTime DataHoraEvento { get; set; }

        //navegacao
        public virtual Moto Moto { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
