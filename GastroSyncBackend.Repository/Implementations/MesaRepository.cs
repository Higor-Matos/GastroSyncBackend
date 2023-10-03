using GastroSyncBackend.Domain.DTOs;
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
        _dbContext.Consumidores!.RemoveRange(mesa.Consumidores!);
        _dbContext.Mesas!.Remove(mesa);
        await _dbContext.SaveChangesAsync();
        return true;

    }

    public async Task<bool> RemoveTodasMesasEReiniciaId()
    {
        var hasMesas = await _dbContext.Mesas!.AnyAsync();
        if (!hasMesas) return false;
        _dbContext.Consumidores!.RemoveRange(_dbContext.Consumidores);
        _dbContext.Mesas!.RemoveRange(_dbContext.Mesas);
        await _dbContext.SaveChangesAsync();

        ResetarContadorID(typeof(MesaEntity));
        ResetarContadorID(typeof(ConsumidorEntity));

        return true;

    }

    private void ResetarContadorID(Type entityType)
    {
        var tableName = _dbContext.Model.FindEntityType(entityType)!.GetTableName();
        var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
        _dbContext.Database.ExecuteSqlRaw(sql);
    }

    private IQueryable<MesaEntity> IncludeConsumidores() =>
        _dbContext.Mesas!.Include(m => m.Consumidores);

    public async Task<IEnumerable<MesaEntity>> ObterTodasAsMesas()
    {
        return await _dbContext.Mesas!
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)!
            .ThenInclude(p => p.Divisoes)
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)!
            .ThenInclude(p => p.Produto)
            .ToListAsync();
    }


    public async Task<MesaEntity?> ObterMesaPorNumero(int numeroMesa) =>
        await IncludeConsumidores()
            .FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesa);
}

