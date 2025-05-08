using Motos.Domain.Entities;
using Motos.Domain.Entities.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_EVENTO")]
    public class Evento
    {
        [Key]
        [Column(TypeName = "NUMBER")]
        public int IdEvento { get; set; }

        [Column(TypeName = "VARCHAR2(500)")]
        public string DescricaoEvento { get; set; }

        [Column(TypeName = "VARCHAR2(50)")]
        public CorEvento CorEvento { get; set; }

        //navegacao
        public ICollection<EventoMoto>? EventosMoto { get; set; } = new Collection<EventoMoto>();
    }
}
