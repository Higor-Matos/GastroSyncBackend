using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IConsumidorRepository
{
    Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);
    Task<List<ConsumidorEntity>?> ObterConsumidoresMesa(int mesaNumero);
    Task<ConsumoIndividualDTO?> ObterConsumoIndividualMesa(int mesaNumero, int consumidorId);
}