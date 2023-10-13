using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts;

public abstract class AppDbContextBase : DbContext
{
    private readonly ILogger<AppDbContextBase> _logger;

    public DbSet<ProdutoEntity>? Produtos { get; set; }

    protected AppDbContextBase(DbContextOptions options, ILogger<AppDbContextBase> logger) : base(options)
    {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        try
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

            modelBuilder.Entity<ConfiguracaoEstabelecimentoEntity>()
                .Property(p => p.ValorCover)
                .HasColumnType("decimal(10,2)")
                .HasPrecision(10, 2);

            modelBuilder.Entity<ConfiguracaoEstabelecimentoEntity>().HasData(
                new ConfiguracaoEstabelecimentoEntity { Id = 1, UsarCover = false, ValorCover = 15m }
            );

            SeedData.Seed(modelBuilder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao configurar o modelo do banco de dados.");
            throw;
        }
    }
}