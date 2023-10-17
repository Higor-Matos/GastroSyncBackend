namespace GastroSyncBackend.Domain.DTOs;

public class PagamentoDetalhadoDto
{
    public int Id { get; set; }
    public int ConsumidorId { get; set; }
    public string ConsumidorNome { get; set; }
    public decimal ValorPago { get; set; }
    public DateTime DataPagamento { get; set; }
    public List<string> PedidosPagos { get; set; }
}