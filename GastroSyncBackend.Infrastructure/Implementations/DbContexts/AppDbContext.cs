using GastroSyncBackend.Domain.Implementations.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Produto>? Produtos { get; set; }
    private readonly ILogger<AppDbContext> _logger;

    public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
    {
        _logger = logger;
        _logger.LogInformation("AppDbContext foi instanciado");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _logger.LogInformation("AppDbContext: Iniciando a configuração do modelo de entidade");

        try
        {
            ConfigureProdutoEntity(modelBuilder);

            SeedData.Seed(modelBuilder);

            _logger.LogInformation("AppDbContext: Configuração do modelo de entidade concluída com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError("AppDbContext: Erro durante a configuração do modelo de entidade - {ErrorMessage}", ex.Message);
        }
    }

    private static void ConfigureProdutoEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Produto>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasColumnType("decimal(18,2)");
    }



    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
}