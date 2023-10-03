namespace GastroSyncBackend.Domain.DTOs;

public class DivisaoProdutoDTO
{
    public int ConsumidorId { get; set; }
    public string? ConsumidorNome { get; set; }
    public decimal ValorDividido { get; set; }
    public string? NomeProduto { get; set; } 
    public int QuantidadeProduto { get; set; }  
    public int TotalDivisoes { get; set; } 
}