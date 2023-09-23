using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;
[AutoDI]
public interface IMesaRepository
{
    Task<MesaEntity> CreateMesaAsync(int numeroMesa, string local);
    Task<bool> MesaExisteAsync(int numeroMesa);
    Task AddConsumidoresAsync(int mesaId, List<string> consumidores);
}