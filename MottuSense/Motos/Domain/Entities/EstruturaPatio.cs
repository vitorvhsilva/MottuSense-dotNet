using System.Numerics;

namespace Motos.Domain.Entitites
{
    public class EstruturaPatio
    {
        private string IdPatio { get; set; }
        private int CoordenadaXEstrutura { get; set; }
        private int CoordenadaYEstrutura { get; set; }
        private double TamanhoEstrutura { get; set; }
        private double RotacaoEstrutura { get; set; }
    }
}
