using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;

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
    public void EnsureDatabaseCreated()
    {
        this.Database.EnsureCreated();
    }

    public void DatabaseMigrate()
    {
        this.Database.Migrate();
    }

    public DbSet<MesaEntity> Mesas { get; set; }

}

