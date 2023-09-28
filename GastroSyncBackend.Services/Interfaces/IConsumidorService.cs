using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IConsumidorService
{
    Task<ServiceResponse<bool>> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);
    Task<ServiceResponse<List<ConsumidorEntity>>> ObterConsumidoresMesa(int mesaNumero);
    Task<ServiceResponse<ConsumoIndividualDTO>> ObterConsumoIndividualMesa(int mesaNumero, int consumidorId);
}