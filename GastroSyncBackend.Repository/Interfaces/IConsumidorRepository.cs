using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IConsumidorRepository
{
    Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);
    Task<bool> AtualizarConsumidor(ConsumidorEntity consumidor);
    Task<ConsumidorEntity?> ObterConsumidorPorId(int id);
}