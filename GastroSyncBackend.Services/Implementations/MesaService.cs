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

    public List<MesaEntity> GetAllMesas()
    {
        return _context.Mesas!.ToList();
    }

}