using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;
    private readonly IAppDbContext _context;

    public MesaService(IMesaRepository mesaRepository, IAppDbContext context)
    {
        _mesaRepository = mesaRepository;
        _context = context;

    }

    public async Task<MesaEntity> CreateMesaAsync(int numeroMesa)
    {
        var mesa = new MesaEntity { NumeroMesa = numeroMesa };
        return await _mesaRepository.CreateMesaAsync(mesa);
    }

    public async Task<List<MesaEntity>> GetAllMesas()
    {
        return await _context.Mesas!.ToListAsync();
    }

    public async Task<MesaEntity?> GetMesaById(int id)
    {
        return await _context.Mesas!.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> RemoveMesaById(int id)
    {
        var mesa = await _context.Mesas!.FirstOrDefaultAsync(m => m.Id == id);
        if (mesa == null) return false;
        _context.Mesas!.Remove(mesa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task RemoveAllMesasAndResetId()
    {
        _context.Mesas!.RemoveRange(_context.Mesas);
        await _context.SaveChangesAsync();

        // Redefine o contador de identidade
        var entityType = _context.Model.FindEntityType(typeof(MesaEntity));
        var tableName = entityType!.GetTableName();
        var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
        _context.Database.ExecuteSqlRaw(sql);
    }

    public async Task<bool> MesaExisteAsync(int numeroMesa)
    {
        return await _mesaRepository.MesaExisteAsync(numeroMesa);
    }

}