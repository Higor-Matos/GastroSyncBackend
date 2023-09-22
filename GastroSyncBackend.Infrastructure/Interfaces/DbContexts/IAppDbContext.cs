using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Infrastructure.Interfaces.DbContexts;

[AutoDI]
public interface IAppDbContext
{
    DbSet<Produto>? Produtos { get; set; }
    void EnsureDatabaseCreated();
}
