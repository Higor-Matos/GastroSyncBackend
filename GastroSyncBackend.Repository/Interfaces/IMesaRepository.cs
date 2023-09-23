using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;
[AutoDI]
public interface IMesaRepository
{
    Task<MesaEntity> CreateMesaAsync(MesaEntity mesa);
    Task<bool> MesaExisteAsync(int numeroMesa);

}