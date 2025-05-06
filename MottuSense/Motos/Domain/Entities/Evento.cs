using Motos.Domain.Entities;
using Motos.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_EVENTO")]
    public class Evento
    {
        [Key]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdEvento { get; set; }

        [Column(TypeName = "VARCHAR2(500)")]
        public string DescricaoEvento { get; set; }

        [Column(TypeName = "VARCHAR2(50)")]
        public CorEvento CorEvento { get; private set; }


        public ICollection<EventoMoto> EventosMoto { get; set; }
    }
}
