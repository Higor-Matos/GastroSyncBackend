using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts;

public class AppDbContext : AppDbContextBase, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
    public async Task EnsureDatabaseCreatedAsync()
    {
        await Database.EnsureCreatedAsync();
    }

    public async Task DatabaseMigrateAsync()
    {
        await Database.MigrateAsync();
    }

    public DbSet<MesaEntity>? Mesas { get; set; }
    public new DatabaseFacade Database => base.Database;
    public new IModel Model => base.Model;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProdutoEntity>()
            .Property(p => p.Preco)
            .HasColumnType("decimal(18,2)")
            .HasPrecision(18, 2);
    }

}