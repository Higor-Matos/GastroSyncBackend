using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class ConsumidorRepository : IConsumidorRepository
{
    private readonly IAppDbContext _dbContext;

    public ConsumidorRepository(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores)
    {
        var mesa = await ObterMesaPorNumero(mesaId);
        if (mesa == null) return false;

        var consumidorEntities = consumidores.Select(nome => new ConsumidorEntity { Nome = nome, MesaId = mesaId }).ToList();
        mesa.Consumidores!.AddRange(consumidorEntities);
        await _dbContext.SaveChangesAsync();

        return true;
    }
    private async Task<MesaEntity?> ObterMesaPorNumero(int mesaNumero) =>
        await _dbContext.Mesas!
            .Include(m => m.Consumidores)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);
}