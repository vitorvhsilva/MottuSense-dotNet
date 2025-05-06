using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_PATIO")]
    public class Patio
    {
        [Key]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdPatio { get; set; }
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdFilial { get; set; }
        [Column(TypeName = "NUMBER(1)")]
        public bool EstruturaPatioCriada { get; set; }
        public ICollection<Moto> Motos { get; set; }
    }
}
