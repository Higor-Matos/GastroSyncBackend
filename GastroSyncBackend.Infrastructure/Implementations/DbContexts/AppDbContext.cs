using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GastroSyncBackend.Infrastructure.Implementations.DbContexts
{
    public class AppDbContext : AppDbContextBase, IAppDbContext
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public new DbSet<ProdutoEntity>? Produtos { get; set; }
        public DbSet<PedidoEntity>? Pedidos { get; set; }
        public DbSet<PagamentoEntity>? Pagamentos { get; set; }
        public DbSet<MesaEntity>? Mesas { get; set; }
        public DbSet<ConsumidorEntity>? Consumidores { get; set; }
        public DbSet<DivisaoProdutoEntity>? DivisoesProdutos { get; set; }
        public DbSet<ConfiguracaoEstabelecimentoEntity>? ConfiguracaoEstabelecimento { get; set; }

        public async Task EnsureDatabaseCreatedAsync()
        {
            try
            {
                await Database.EnsureCreatedAsync();
                Logger.Info("Banco de dados criado com sucesso.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao criar o banco de dados.");
                throw;
            }
        }

        public async Task DatabaseMigrateAsync()
        {
            try
            {
                Logger.Info("Configuração de warnings aplicada, iniciando migração do banco de dados.");

                await Database.MigrateAsync();

                Logger.Info("Banco de dados migrado com sucesso.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao migrar o banco de dados. Verifique se as configurações estão corretas e se o banco de dados está acessível.");
                throw;
            }
        }


        public new async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await base.SaveChangesAsync(cancellationToken);
                Logger.Info("Alterações no banco de dados salvas com sucesso.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao salvar as alterações no banco de dados.");
                throw;
            }
        }

        public new async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            try
            {
                var dbSet = Set<TEntity>();
                return await dbSet.FindAsync(keyValues);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao buscar entidade no banco de dados.");
                throw;
            }
        }
    }
}
