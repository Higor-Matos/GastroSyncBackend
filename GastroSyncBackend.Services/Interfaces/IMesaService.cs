using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IMesaService
{
    Task<MesaEntity> CreateMesaAsync(int numeroMesa, string local);

    Task<List<MesaEntity>> GetAllMesas();
    Task<MesaEntity?> GetMesaById(int id);
    Task<bool> RemoveMesaById(int id);
    Task RemoveAllMesasAndResetId();
    Task<bool> MesaExisteAsync(int numeroMesa);
}