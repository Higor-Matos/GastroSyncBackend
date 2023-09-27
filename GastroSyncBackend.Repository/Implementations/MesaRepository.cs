using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class MesaRepository : IMesaRepository
{
    private readonly IAppDbContext _dbContext;

    public MesaRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MesaEntity?> CreateMesaAsync(int numeroMesa, string local)
    {
        var mesa = new MesaEntity { NumeroMesa = numeroMesa, Local = local };
        _dbContext.Mesas!.Add(mesa);
        await _dbContext.SaveChangesAsync();
        return mesa;
    }



    public async Task<bool> AddConsumidoresAsync(int mesaId, List<string> consumidores)
    {
        var mesa = await _dbContext.Mesas!.FirstOrDefaultAsync(m => m.NumeroMesa == mesaId);
        if (mesa == null) return false;
        var consumidorEntities = consumidores.Select(nome => new ConsumidorEntity { Nome = nome, MesaId = mesaId })
            .ToList();
        mesa.Consumidores!.AddRange(consumidorEntities);
        await _dbContext.SaveChangesAsync();
        return true;

    }




    public async Task<bool> MesaExisteAsync(int numeroMesa)
    {
        return await _dbContext.Mesas!.AnyAsync(m => m.NumeroMesa == numeroMesa);
    }
}
