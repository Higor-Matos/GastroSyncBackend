using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IMesaService
{
    Task<ServiceResponse<MesaEntity>> CriarMesa(int numeroMesa, string local);
    Task<ServiceResponse<bool>> RemoveMesaPeloNumero(int mesaNumber);
    Task<ServiceResponse<bool>> RemoveTodasMesasEReiniciaId();
    Task<ServiceResponse<IEnumerable<MesaEntity>>> ObterTodasAsMesas();
    Task<ServiceResponse<MesaEntity>> ObterMesaPorNumero(int numeroMesa);
    Task<ServiceResponse<bool>> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores);
    Task<ServiceResponse<List<ConsumidorEntity>>> ObterConsumidoresMesa(int mesaNumero);
}