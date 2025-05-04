using Motos.Domain.Entities.Enums;

namespace Motos.Domain.Entitites
{
    public class Moto
    {
        public string IdMoto { get; set; }
        public string PlacaMoto { get; set; }
        public string ModeloMoto { get; set; }
        public StatusMoto StatusMoto { get; set; }
        public string ChassiMoto { get; set; }
        public string IotMoto { get; set; }
    }
}
