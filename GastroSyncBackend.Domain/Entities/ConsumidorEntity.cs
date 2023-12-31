﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("Consumidores")]
public class ConsumidorEntity
{
    public int? Id { get; set; }
    public string? Nome { get; set; }
    public int? MesaId { get; set; }
    public MesaEntity? Mesa { get; set; }
    public decimal TotalConsumido { get; set; } = 0;
    public List<PedidoEntity>? Pedidos { get; set; }

}