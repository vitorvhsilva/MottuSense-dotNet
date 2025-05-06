using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_ESTRUTURA_PATIO")]
    public class EstruturaPatio
    {
        [Key]
        [ForeignKey("Patio")]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdPatio { get; set; }
        [Column(TypeName = "NUMBER")]
        public int CoordenadaXEstrutura { get; set; }
        [Column(TypeName = "NUMBER")]
        public int CoordenadaYEstrutura { get; set; }
        [Column(TypeName = "NUMBER")]
        public double TamanhoEstrutura { get; set; }
        [Column(TypeName = "NUMBER")]
        public double RotacaoEstrutura { get; set; }

        //navegacao
        public virtual Patio Patio { get; set; }
    }
}
