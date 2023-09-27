using System.Collections;
using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IMesaService
{ Task<ServiceResponse<MesaEntity>> CreateMesaAsync(int numeroMesa, string local);
    Task<bool> RemoveMesaByMesaNumber(int mesaNumber);
    Task<ServiceResponse<bool>> RemoveAllMesasAndResetId();
    Task<ServiceResponse<IEnumerable<MesaEntity>>> ObterTodasAsMesasAsync();
    Task<ServiceResponse<MesaEntity>> ObterMesaPorNumeroAsync(int numeroMesa);
    Task<bool> AddConsumidoresAsync(int mesaId, List<string> consumidores);
}