namespace GastroSyncBackend.Domain.DTOs;

public class MesaDto
{
    public int? Id { get; set; }
    public int? NumeroMesa { get; set; }
    public string? Local { get; set; }
    public List<ConsumidorDto>? Consumidores { get; set; }
}
