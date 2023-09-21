using GastroSyncBackend.Domain.Implementations.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Produto> Produtos { get; set; }
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
            if (Produtos != null)
            {
                modelBuilder.Entity<Produto>()
                    .Property(p => p.Preco)
                    .HasColumnType("decimal(18,2)");
            }

            // Chamando o método SeedData para semear os dados
            SeedData(modelBuilder);

            _logger.LogInformation("AppDbContext: Configuração do modelo de entidade concluída com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError("AppDbContext: Erro durante a configuração do modelo de entidade - {ErrorMessage}", ex.Message);
        }
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        _logger.LogInformation("Semeadura inicial de dados...");

        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = 1, Nome = "Pizza", Preco = 50.5M, Categoria = "Comida" },
            new Produto { Id = 2, Nome = "Sushi", Preco = 70, Categoria = "Comida" },
            new Produto { Id = 3, Nome = "Macarrão", Preco = 30.5M, Categoria = "Comida" },
            new Produto { Id = 4, Nome = "Hamburger", Preco = 35.9M, Categoria = "Comida" },
            new Produto { Id = 5, Nome = "Suco", Preco = 4.5M, Categoria = "Bebida" },
            new Produto { Id = 6, Nome = "Água", Preco = 3.5M, Categoria = "Bebida" },
            new Produto { Id = 7, Nome = "Refrigerante", Preco = 10.9M, Categoria = "Bebida" },
            new Produto { Id = 8, Nome = "Cerveja", Preco = 15M, Categoria = "Bebida" }
        );
    }

    public void EnsureDatabaseCreated()
    {
        this.Database.EnsureCreated();
    }
}