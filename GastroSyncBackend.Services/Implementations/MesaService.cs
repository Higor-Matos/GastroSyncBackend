using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }

    public async Task<MesaEntity> CreateMesaAsync(int numeroMesa)
    {
        var mesa = new MesaEntity { NumeroMesa = numeroMesa };
        return await _mesaRepository.CreateMesaAsync(mesa);
    }
}