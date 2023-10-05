using System.ComponentModel.DataAnnotations.Schema;

namespace GastroSyncBackend.Domain.Entities;

[Table("ConfiguracaoEstabelecimento")]
public class ConfiguracaoEstabelecimentoEntity
{
    public int Id { get; set; }
    public bool UsarCover { get; set; }
    public decimal ValorCover { get; set; }
}