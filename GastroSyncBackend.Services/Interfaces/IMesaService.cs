using System.Collections;
using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IMesaService
{
    Task<MesaEntity> CreateMesaAsync(int numeroMesa, string local);
    Task RemoveAllMesasAndResetId();
    Task<bool> MesaExisteAsync(int numeroMesa);
    Task<bool> AddConsumidoresAsync(int mesaId, List<string> consumidores);
    Task<bool> RemoveMesaByMesaNumber(int mesaNumber);
    Task<IEnumerable> GetAllMesas();
    Task<MesaEntity> GetMesaByNumero(int numeroMesa);
    Task<ServiceResponse<IEnumerable<MesaEntity>>> ObterTodasAsMesasAsync();
    Task<ServiceResponse<MesaEntity>> ObterMesaPorNumeroAsync(int numeroMesa);


}