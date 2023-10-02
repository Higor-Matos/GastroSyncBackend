namespace GastroSyncBackend.Domain.Request;

public class AddPedidoRequestBase
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}