using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repositories
{
    public class ConfiguracaoEstabelecimentoRepository : IConfiguracaoEstabelecimentoRepository
    {
        private readonly IAppDbContext _dbContext;

        public ConfiguracaoEstabelecimentoRepository(IAppDbContext dbContext) => _dbContext = dbContext;

        public async Task<ConfiguracaoEstabelecimentoEntity> ObterConfiguracaoAsync() => (await _dbContext.ConfiguracaoEstabelecimento.FirstOrDefaultAsync())!;

        public async Task<bool> AtualizarConfiguracaoAsync(ConfiguracaoEstabelecimentoEntity configuracao)
        {
            _dbContext.ConfiguracaoEstabelecimento.Update(configuracao);

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}