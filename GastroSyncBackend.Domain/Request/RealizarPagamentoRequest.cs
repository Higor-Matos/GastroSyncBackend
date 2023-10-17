namespace GastroSyncBackend.Domain.Request;
public class RealizarPagamentoRequest
{
    public int ConsumidorId { get; set; }
    public decimal Valor { get; set; }
    public int NumeroMesa { get; set; }
}

