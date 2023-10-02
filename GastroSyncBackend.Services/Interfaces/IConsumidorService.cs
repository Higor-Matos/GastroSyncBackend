using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IConsumidorService
{
    Task<ServiceResponse<bool>> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);

}