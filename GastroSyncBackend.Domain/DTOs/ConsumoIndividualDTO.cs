namespace GastroSyncBackend.Domain.DTOs;

public class ConsumoIndividualDTO
{
    public int ConsumidorId { get; set; }
    public string? ConsumidorNome { get; set; } 
    public decimal TotalIndividual { get; set; }
    public List<ProdutoDTO>? ProdutosConsumidos { get; set; }
}