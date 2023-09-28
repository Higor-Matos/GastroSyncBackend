namespace GastroSyncBackend.Domain.DTOs;

public class ConsumoMesaDTO
{
    public int MesaNumero { get; set; }
    public decimal TotalMesa { get; set; }
    public List<ConsumoIndividualDTO>? Consumidores { get; set; }
}