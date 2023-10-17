using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("HistoricosPagamentos")]
public class HistoricoPagamentoEntity
{
    public int Id { get; set; }
    public int ConsumidorId { get; set; }
    public ConsumidorEntity? Consumidor { get; set; }
    public decimal ValorPago { get; set; }
    public DateTime DataPagamento { get; set; }
    public List<DivisaoProdutoEntity>? DivisoesPagas { get; set; }
}