using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repositories.Interfaces;
[AutoDI]
public interface IConfiguracaoEstabelecimentoRepository
{
    Task<ConfiguracaoEstabelecimentoEntity> ObterConfiguracaoAsync();
    Task<bool> AtualizarConfiguracaoAsync(ConfiguracaoEstabelecimentoEntity configuracao);
}