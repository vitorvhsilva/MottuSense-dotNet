using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entitites
{
    [Table("TB_ESTRUTURA_PATIO")]
    public class EstruturaPatio
    {
        [Key]
        [ForeignKey("Patio")]
        public string IdPatio { get; set; }
        public int CoordenadaXEstrutura { get; set; }
        public int CoordenadaYEstrutura { get; set; }
        public double TamanhoEstrutura { get; set; }
        public double RotacaoEstrutura { get; set; }

        //navegacao
        public virtual Patio Patio { get; set; }
    }
}
