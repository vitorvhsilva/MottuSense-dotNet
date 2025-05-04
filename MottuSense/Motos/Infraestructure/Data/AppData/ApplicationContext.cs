using Microsoft.EntityFrameworkCore;
using Motos.Domain.Entities;
using Motos.Domain.Entitites;

namespace Motos.Infraestructure.Data.AppData
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }

        public DbSet<Patio> Patio { get; set; }
        public DbSet<Moto> Moto { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<EventoMoto> EventoMoto { get; set; }
        public DbSet<LocalizacaoMoto> LocalizacaoMoto { get; set; }
        public DbSet<EstruturaPatio> EstruturaPatio { get; set; }
    }
}
