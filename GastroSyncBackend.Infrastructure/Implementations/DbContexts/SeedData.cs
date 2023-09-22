using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = 1, Nome = "Pizza", Preco = 50.5M, Categoria = "Comida" },
            new Produto { Id = 2, Nome = "Sushi", Preco = 70, Categoria = "Comida" },
            new Produto { Id = 3, Nome = "Macarrão", Preco = 30.5M, Categoria = "Comida" },
            new Produto { Id = 4, Nome = "Hamburger", Preco = 35.9M, Categoria = "Comida" },
            new Produto { Id = 5, Nome = "Suco", Preco = 4.5M, Categoria = "Bebida" },
            new Produto { Id = 6, Nome = "Água", Preco = 3.5M, Categoria = "Bebida" },
            new Produto { Id = 7, Nome = "Refrigerante", Preco = 10.9M, Categoria = "Bebida" },
            new Produto { Id = 8, Nome = "Cerveja", Preco = 15M, Categoria = "Bebida" },
            new Produto { Id = 100, Nome = "Cover", Preco = 15M, Categoria = "Cover" }
        );
    }
}