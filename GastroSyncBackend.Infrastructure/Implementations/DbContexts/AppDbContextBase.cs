using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts;

public abstract class AppDbContextBase : DbContext
{
    public DbSet<ProdutoEntity>? Produtos { get; set; }

    protected AppDbContextBase(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData.Seed(modelBuilder);
    }
}