using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("Mesas")]
public class MesaEntity
{
    public int? Id { get; set; }
    public int? NumeroMesa { get; set; }
    public string? Local { get; set; }
    public List<ConsumidorEntity>? Consumidores { get; set; } = new();
}
