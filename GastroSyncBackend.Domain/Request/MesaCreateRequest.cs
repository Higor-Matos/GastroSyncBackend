namespace GastroSyncBackend.Domain.Request;

public class MesaCreateRequest
{
    public int NumeroMesa { get; set; }
    public string? Local { get; set; }
    public List<string>? Consumidores { get; set; }
}