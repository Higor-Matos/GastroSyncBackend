using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using System.Threading.Tasks;

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

    Task<bool> AddPedidoAsync(int mesaId, int consumidorId, int produtoId, int quantidade);

    Task<List<ConsumidorEntity>?> ObterConsumidoresMesa(int mesaNumero);

    Task<ConsumoMesaDTO?> GetConsumoMesa(int mesaNumero);
    Task<ConsumoIndividualDTO?> GetConsumoIndividual(int mesaNumero, int consumidorId);

}