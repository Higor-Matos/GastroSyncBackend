using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("Pedidos")]
public class PedidoEntity
{
    public int Id { get; set; }
    public int ConsumidorId { get; set; }
    public ConsumidorEntity? Consumidor { get; set; }
    public int ProdutoId { get; set; }
    public ProdutoEntity? Produto { get; set; }
    public int Quantidade { get; set; }
}