namespace GastroSyncBackend.Domain.Request;

public class AddPedidoDivididoRequest : AddPedidoRequestBase
{
    public int[] ConsumidoresIds { get; set; } = null!;
}