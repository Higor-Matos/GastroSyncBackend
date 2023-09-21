using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Implementations.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Infrastructure.Interfaces.DbContexts;

[AutoDI]
public interface IAppDbContext
{
    DbSet<Produto>? Produtos { get; set; }
    void EnsureDatabaseCreated();
}
