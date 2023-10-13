using GastroSyncBackend.Domain.Entities;

public interface IPagamentoRepository
{
    Task<PagamentoEntity> CriarPagamento(PagamentoEntity pagamento);
    Task<IEnumerable<PagamentoEntity>> ObterPagamentosPorConsumidor(int consumidorId);
}