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
        public string IdEvento { get; set; }
        public string DescricaoEvento { get; set; }
        public CorEvento CorEvento { get; set; }

        //navegacao
        public ICollection<EventoMoto> EventosMoto { get; set; }
    }
}
