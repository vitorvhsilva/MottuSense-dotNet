using Microsoft.EntityFrameworkCore;
using Motos.Domain.Entities;
using Motos.Domain.Entities.Enums;
using Motos.Domain.Entitites;

namespace Motos.Infraestructure.Data.AppData
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //indicando enum do evento 
            modelBuilder.Entity<Evento>()
                .Property(e => e.CorEvento)
                .HasConversion(
                    v => v.ToString(),
                    v => (CorEvento)Enum.Parse(typeof(CorEvento), v))
                .HasColumnType("VARCHAR2(50)");
            

            //inserindo dados base de patio
            modelBuilder.Entity<Patio>().HasData(
                new Patio { IdPatio = "idTeste", IdFilial = "idTeste", EstruturaPatioCriada = false},
                new Patio { IdPatio = "idTeste2", IdFilial = "idTeste2", EstruturaPatioCriada = false}
            );

            modelBuilder.Entity<Evento>().HasData(
                new Evento { IdEvento = 1, DescricaoEvento = "A moto está preparada para ser alugada", CorEvento = CorEvento.VERDE},
                new Evento { IdEvento = 2, DescricaoEvento = "A moto chegou no patio", CorEvento = CorEvento.CINZA},
                new Evento { IdEvento = 3, DescricaoEvento = "A moto saiu do patio", CorEvento = CorEvento.CINZA},
                new Evento { IdEvento = 4, DescricaoEvento = "A moto está em manutenção", CorEvento = CorEvento.CINZA},
                new Evento { IdEvento = 5, DescricaoEvento = "A moto chegou sem placa", CorEvento = CorEvento.VERMELHO},
                new Evento { IdEvento = 6, DescricaoEvento = "A moto chegou precisando de manutenção", CorEvento = CorEvento.VERMELHO}
            );
        }

        public DbSet<Patio> Patio { get; set; }
        public DbSet<Moto> Moto { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<EventoMoto> EventoMoto { get; set; }
        public DbSet<LocalizacaoMoto> LocalizacaoMoto { get; set; }
        public DbSet<EstruturaPatio> EstruturaPatio { get; set; }
    }
}
