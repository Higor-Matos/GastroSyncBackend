namespace GastroSyncBackend.Domain.DTOs;

public class ConsumidorDTO
{
    public int? Id { get; set; }
    public string? Nome { get; set; }
    public int? MesaId { get; set; }
    public decimal TotalConsumido { get; set; }
    public List<PedidoDTO>? Pedidos { get; set; }
}
