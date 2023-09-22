using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Infrastructure.Interfaces.DbContexts;

[AutoDI]
public interface IAppDbContext
{
    DbSet<ProdutoEntity>? Produtos { get; set; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    void EnsureDatabaseCreated();

}