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

    public async Task<MesaEntity> CreateMesaAsync(int numeroMesa, string local)
    {
        var mesa = new MesaEntity { NumeroMesa = numeroMesa, Local = local };
        _dbContext.Mesas!.Add(mesa);
        await _dbContext.SaveChangesAsync();
        return mesa;
    }


    public async Task<bool> MesaExisteAsync(int numeroMesa)
    {
        return await _dbContext.Mesas!.AnyAsync(m => m.NumeroMesa == numeroMesa);
    }
}
