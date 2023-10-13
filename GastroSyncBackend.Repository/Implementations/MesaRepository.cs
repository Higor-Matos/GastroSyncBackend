using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class MesaRepository : IMesaRepository
{
    private readonly IAppDbContext _dbContext;

    public MesaRepository(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<MesaEntity?> CriarMesa(int numeroMesa, string local)
    {
        var mesa = new MesaEntity { NumeroMesa = numeroMesa, Local = local };
        _dbContext.Mesas!.Add(mesa);
        await _dbContext.SaveChangesAsync();
        return mesa;
    }

    public async Task<bool> RemoveMesaPeloNumero(int mesaNumber)
    {
        var mesa = await ObterMesaPorNumero(mesaNumber);
        if (mesa == null) return false;

        RemoverRelacionados(mesa.Consumidores!);
        _dbContext.Mesas!.Remove(mesa);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveTodasMesasEReiniciaId()
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
        return true;
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
        var tableName = _dbContext.Model.FindEntityType(entityType)!.GetTableName();
        var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
        _dbContext.Database.ExecuteSqlRaw(sql);
    }

    public async Task<IEnumerable<MesaEntity>> ObterTodasAsMesas()
    {
        return await IncludeConsumidores().ToListAsync();
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
        return await IncludeConsumidores().FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesa);
    }
}
