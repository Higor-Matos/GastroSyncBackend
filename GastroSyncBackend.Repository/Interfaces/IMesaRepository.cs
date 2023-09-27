using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;
[AutoDI]
public interface IMesaRepository
{
    Task<MesaEntity?> CriarMesa(int numeroMesa, string local);
    Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);
    Task<IEnumerable<MesaEntity>> ObterTodasAsMesas();
    Task<MesaEntity?> ObterMesaPorNumero(int numeroMesa);
    Task<bool> RemoveMesaPeloNumero(int mesaNumber);
    Task<bool> RemoveTodasMesasEReiniciaId();

    Task<List<ConsumidorEntity>?> ObterConsumidoresMesa(int mesaNumero);
}