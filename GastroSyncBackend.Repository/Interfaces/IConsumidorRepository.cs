using GastroSyncBackend.Common;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IConsumidorRepository
{
    Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);
}