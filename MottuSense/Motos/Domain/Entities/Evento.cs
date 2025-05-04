using Motos.Domain.Entities.Enums;

namespace Motos.Domain.Entitites
{
    public class Evento
    {
        public string IdEvento { get; set; }
        public string DescricaoEvento { get; set; }
        public CorEvento CorEvento { get; set; }
    }
}
