using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts
{
    public abstract class AppDbContextBase : DbContext
    {
        public DbSet<ProdutoEntity>? Produtos { get; set; }

        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações de tipo de coluna para evitar avisos de truncagem
            modelBuilder.Entity<ConsumidorEntity>()
                .Property(b => b.TotalConsumido)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DivisaoProdutoEntity>()
                .Property(b => b.ValorDividido)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<MesaEntity>()
                .Property(b => b.TotalConsumido)
                .HasColumnType("decimal(18, 2)");

            // Chamar o método Seed para preencher dados iniciais, se existir
            SeedData.Seed(modelBuilder);
        }
    }
}