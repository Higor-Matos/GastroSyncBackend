namespace GastroSyncBackend.Domain.DTOs
{
    public class PagamentoDTO
    {
        public int ConsumidorId { get; set; }
        public decimal ValorPago { get; set; }
        public string? MetodoPagamento { get; set; }
        public bool PagamentoConfirmado { get; set; }
    }
}