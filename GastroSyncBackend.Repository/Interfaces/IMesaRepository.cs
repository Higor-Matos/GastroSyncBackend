using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IMesaRepository
{
    Task<MesaEntity?> CriarMesa(int numeroMesa, string local);
    Task<IEnumerable<MesaEntity>> ObterTodasAsMesas();
    Task<MesaEntity?> ObterMesaPorNumero(int numeroMesa);
    Task<bool> RemoveMesaPeloNumero(int mesaNumber);
    Task<bool> RemoveTodasMesasEReiniciaId();
    Task<ConsumoMesaDTO?> ObterConsumoTotalMesa(int mesaNumero);
    
}