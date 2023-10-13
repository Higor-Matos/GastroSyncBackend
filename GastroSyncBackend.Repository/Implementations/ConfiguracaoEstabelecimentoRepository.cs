using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Repository.Implementations;

public class ConfiguracaoEstabelecimentoRepository : IConfiguracaoEstabelecimentoRepository
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<ConfiguracaoEstabelecimentoRepository> _logger;

    public ConfiguracaoEstabelecimentoRepository(IAppDbContext dbContext, ILogger<ConfiguracaoEstabelecimentoRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ConfiguracaoEstabelecimentoEntity> ObterConfiguracaoAsync()
    {
        try
        {
            return (await _dbContext.ConfiguracaoEstabelecimento!
                .OrderBy(x => x.Id)
                .AsSplitQuery()
                .FirstOrDefaultAsync())!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter a configuração do estabelecimento.");
            throw;
        }
    }

    public async Task<bool> AtualizarConfiguracaoAsync(ConfiguracaoEstabelecimentoEntity configuracao)
    {
        try
        {
            _dbContext.ConfiguracaoEstabelecimento!.Update(configuracao);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Configuração do estabelecimento atualizada com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar a configuração do estabelecimento.");
            return false;
        }
    }
}