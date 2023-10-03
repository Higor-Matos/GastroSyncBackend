namespace GastroSyncBackend.Domain.DTOs;

public class MesaDTO
{
    public int? Id { get; set; }
    public int? NumeroMesa { get; set; }
    public string? Local { get; set; }
    public List<ConsumidorDTO>? Consumidores { get; set; }
    public decimal TotalConsumidoMesa { get; set; }

}