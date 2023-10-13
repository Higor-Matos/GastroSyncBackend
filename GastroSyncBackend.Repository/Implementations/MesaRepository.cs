using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Repository.Implementations;

public class MesaRepository : IMesaRepository
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<MesaRepository> _logger;

    public MesaRepository(IAppDbContext dbContext, ILogger<MesaRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<MesaEntity?> CriarMesa(int numeroMesa, string local)
    {
        try
        {
            var mesa = new MesaEntity { NumeroMesa = numeroMesa, Local = local };
            _dbContext.Mesas!.Add(mesa);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Mesa criada com sucesso.");
            return mesa;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar mesa.");
            throw;
        }
    }

    public async Task<bool> RemoveMesaPeloNumero(int mesaNumber)
    {
        try
        {
            var mesa = await ObterMesaPorNumero(mesaNumber);
            if (mesa == null) return false;

            RemoverRelacionados(mesa.Consumidores!);
            _dbContext.Mesas!.Remove(mesa);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Mesa removida com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover mesa.");
            throw;
        }
    }

    public async Task<bool> RemoveTodasMesasEReiniciaId()
    {
        try
        {
            if (!await _dbContext.Mesas!.AnyAsync()) return false;

            var todasMesas = await ObterTodasAsMesas();
            var mesaEntities = todasMesas as MesaEntity[] ?? todasMesas.ToArray();
            foreach (var mesa in mesaEntities)
            {
                RemoverRelacionados(mesa.Consumidores!);
            }
            _dbContext.Mesas!.RemoveRange(mesaEntities);

            await _dbContext.SaveChangesAsync();
            ResetarContadorID(typeof(MesaEntity));
            ResetarContadorID(typeof(ConsumidorEntity));
            ResetarContadorID(typeof(DivisaoProdutoEntity));
            ResetarContadorID(typeof(PedidoEntity));
            _logger.LogInformation("Todas as mesas removidas com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover todas as mesas.");
            throw;
        }
    }

    private void RemoverRelacionados(List<ConsumidorEntity> consumidores)
    {
        consumidores.ForEach(consumidor =>
        {
            _dbContext.DivisoesProdutos!.RemoveRange(_dbContext.DivisoesProdutos.Where(dp => dp.ConsumidorId == consumidor.Id));
            _dbContext.Pedidos!.RemoveRange(_dbContext.Pedidos.Where(p => p.ConsumidorId == consumidor.Id));
        });
        _dbContext.Consumidores!.RemoveRange(consumidores);
    }

    private void ResetarContadorID(Type entityType)
    {
        try
        {
            var tableName = _dbContext.Model.FindEntityType(entityType)!.GetTableName();
            var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
            _dbContext.Database.ExecuteSqlRaw(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao resetar contador de ID.");
            throw;
        }
    }

    public async Task<IEnumerable<MesaEntity>> ObterTodasAsMesas()
    {
        try
        {
            return await IncludeConsumidores().ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter todas as mesas.");
            throw;
        }
    }

    private IQueryable<MesaEntity> IncludeConsumidores() =>
        _dbContext.Mesas!
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)!
            .ThenInclude(p => p.Divisoes)
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)!
            .ThenInclude(p => p.Produto);

    public async Task<MesaEntity?> ObterMesaPorNumero(int numeroMesa)
    {
        try
        {
            return await IncludeConsumidores().FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesa);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter mesa por número.");
            throw;
        }
    }
}