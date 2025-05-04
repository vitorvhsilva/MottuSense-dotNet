using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_PATIO")]
    public class Patio
    {
        [Key]
        public string IdPatio { get; set; }
        public string IdFilial { get; set; }
        public bool EstruturaPatioCriada { get; set; }
        public ICollection<Moto> Motos { get; set; }
    }
}
