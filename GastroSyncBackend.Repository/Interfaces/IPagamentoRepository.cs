using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IPagamentoRepository
{
    Task<PagamentoEntity> CriarPagamento(PagamentoEntity pagamento);
    Task<IEnumerable<PagamentoEntity>> ObterPagamentosPorConsumidor(int consumidorId);
}