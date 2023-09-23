using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("Produtos")]
public class ProdutoEntity
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Categoria { get; set; }
    public decimal Preco { get; set; }
}