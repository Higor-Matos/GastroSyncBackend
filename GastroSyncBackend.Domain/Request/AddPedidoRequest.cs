namespace GastroSyncBackend.Domain.Request;

public class AddPedidoRequest
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}