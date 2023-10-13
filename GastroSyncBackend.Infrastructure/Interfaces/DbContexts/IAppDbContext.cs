using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GastroSyncBackend.Infrastructure.Interfaces.DbContexts
{
    [AutoDI]
    public interface IAppDbContext
    {
        public DbSet<ProdutoEntity>? Produtos { get; set; }
        public DbSet<PedidoEntity>? Pedidos { get; set; }
        public DbSet<PagamentoEntity>? Pagamentos { get; set; }
        public DbSet<MesaEntity>? Mesas { get; set; }
        public DbSet<ConsumidorEntity>? Consumidores { get; set; }
        public DbSet<DivisaoProdutoEntity>? DivisoesProdutos { get; set; }
        public DbSet<ConfiguracaoEstabelecimentoEntity>? ConfiguracaoEstabelecimento { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task EnsureDatabaseCreatedAsync();
        Task DatabaseMigrateAsync();
        Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        DatabaseFacade Database { get; }
        IModel Model { get; }
    }
}