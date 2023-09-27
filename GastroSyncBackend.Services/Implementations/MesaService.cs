using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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


    public async Task<MesaEntity?> CreateMesaAsync(int numeroMesa, string local)
    {
        return await _mesaRepository.CreateMesaAsync(numeroMesa, local);
    }

    public async Task<bool> RemoveMesaByMesaNumber(int mesaNumber)
    {
        var mesa = await _context.Mesas!.Include(m => m.Consumidores)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumber);
        if (mesa == null)
        {
            return false;
        }

        _context.Consumidores!.RemoveRange(mesa.Consumidores!);
        _context.Mesas!.Remove(mesa);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ServiceResponse<bool>> RemoveAllMesasAndResetId()
    {
        var mesas = await _context.Mesas!.ToListAsync();
        if (!mesas.Any())
        {
            return new ServiceResponse<bool>(false, "Nenhuma mesa para remover");
        }

        _context.Consumidores!.RemoveRange(_context.Consumidores);
        _context.Mesas!.RemoveRange(mesas);
        await _context.SaveChangesAsync();

        // Redefine o contador de identidade
        var entityType = _context.Model.FindEntityType(typeof(MesaEntity));
        var tableName = entityType!.GetTableName();
        var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
        _context.Database.ExecuteSqlRaw(sql);

        return new ServiceResponse<bool>(true, "Todas as mesas foram removidas");
    }


    public async Task<IEnumerable> GetAllMesas()
    {
        return await _context.Mesas!.Include(m => m.Consumidores).ToListAsync();
    }

    public async Task<MesaEntity> GetMesaByNumero(int numeroMesa)
    {
        var query = _context.Mesas!.Where(m => m.NumeroMesa == numeroMesa);

        return (await query
            .Include(m => m.Consumidores)
            .FirstOrDefaultAsync())!;
    }

    public async Task<ServiceResponse<IEnumerable<MesaEntity>>> ObterTodasAsMesasAsync()
    {
        var mesas = await _context.Mesas!.Include(m => m.Consumidores).ToListAsync();
        return !mesas.Any()
            ? new ServiceResponse<IEnumerable<MesaEntity>>(false, "Nenhuma mesa disponível")
            : new ServiceResponse<IEnumerable<MesaEntity>>(true, "Operação bem-sucedida", mesas);
    }

    public async Task<ServiceResponse<MesaEntity>> ObterMesaPorNumeroAsync(int numeroMesa)
    {
        var mesa = await _context.Mesas!.Where(m => m.NumeroMesa == numeroMesa)
            .Include(m => m.Consumidores)
            .FirstOrDefaultAsync();

        return mesa == null ? new ServiceResponse<MesaEntity>(false, "Mesa não encontrada") : new ServiceResponse<MesaEntity>(true, "Operação bem-sucedida", mesa);
    }

    public async Task<bool> MesaExisteAsync(int numeroMesa)
    {
        return await _mesaRepository.MesaExisteAsync(numeroMesa);
    }

    public async Task<bool> AddConsumidoresAsync(int mesaId, List<string> consumidores)
    {
        var result = await _mesaRepository.AddConsumidoresAsync(mesaId, consumidores);
        return result;
    }
}