using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GastroSyncBackend.Infrastructure.Interfaces.DbContexts;

[AutoDI]
public interface IAppDbContext
{
    DbSet<ProdutoEntity>? Produtos { get; set; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task EnsureDatabaseCreatedAsync();
    Task DatabaseMigrateAsync();
    DbSet<MesaEntity>? Mesas { get; set; }
    DbSet<ConsumidorEntity>? Consumidores { get; set; }
    DatabaseFacade Database { get; }
    IModel Model { get; }
    DbSet<PedidoEntity> Pedidos { get; set; }
    DbSet<DivisaoProdutoEntity> DivisoesProdutos { get; set; }
    DbSet<ConfiguracaoEstabelecimentoEntity> ConfiguracaoEstabelecimento { get; set; }

}
