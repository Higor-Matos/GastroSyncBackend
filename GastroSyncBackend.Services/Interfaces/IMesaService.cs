using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IMesaService
{
    Task<MesaEntity> CreateMesaAsync(int id);

    List<MesaEntity> GetAllMesas();
    MesaEntity? GetMesaById(int id);


}