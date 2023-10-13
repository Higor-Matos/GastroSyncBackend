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

            modelBuilder.Entity<ConsumidorEntity>()
                .Property(b => b.TotalConsumido)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<DivisaoProdutoEntity>()
                .Property(b => b.ValorDividido)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<MesaEntity>()
                .Property(b => b.TotalConsumidoMesa)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ProdutoEntity>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);

            modelBuilder.Entity<PagamentoEntity>()
                .Property(p => p.ValorPago)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);

            SeedData.Seed(modelBuilder);
        }
    }
}