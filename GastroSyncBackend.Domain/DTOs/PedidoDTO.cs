namespace GastroSyncBackend.Domain.DTOs;

public class PedidoDTO
{
    public int Id { get; set; }
    public int ConsumidorId { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public ProdutoDTO? Produto { get; set; }
    public List<DivisaoProdutoDTO>? Divisoes { get; set; }
}