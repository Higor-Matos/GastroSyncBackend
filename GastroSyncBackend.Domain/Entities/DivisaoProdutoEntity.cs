using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("DivisoesProdutos")]
public class DivisaoProdutoEntity
{
    public int? Id { get; set; }
    public int? PedidoId { get; set; }
    public PedidoEntity? Pedido { get; set; }
    public int? ConsumidorId { get; set; }
    public ConsumidorEntity? Consumidor { get; set; }
    public decimal ValorDividido { get; set; }
}