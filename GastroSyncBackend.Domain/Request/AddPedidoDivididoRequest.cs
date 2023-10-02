namespace GastroSyncBackend.Domain.Request;

public class AddPedidoDivididoRequest
{
    public int[] ConsumidoresIds { get; set; } = null!;
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}
