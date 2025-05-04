using Motos.Domain.Entities;
using Motos.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_MOTO")]
    public class Moto
    {
        [Key]
        public string IdMoto { get; set; }
        public string PlacaMoto { get; set; }
        public string ModeloMoto { get; set; }
        public StatusMoto StatusMoto { get; set; }
        public string ChassiMoto { get; set; }
        public string IotMoto { get; set; }

        [ForeignKey("Moto")]
        public string IdPatio { get; set; }

        //navegacao
        public virtual Patio Patio { get; set; }
        public LocalizacaoMoto LocalizacaoMoto { get; set; }
        public ICollection<EventoMoto> EventosMoto { get; set; }
    }
}
