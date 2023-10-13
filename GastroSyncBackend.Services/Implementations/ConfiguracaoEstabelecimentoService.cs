using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repositories.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services
{
    public class ConfiguracaoEstabelecimentoService : IConfiguracaoEstabelecimentoService
    {
        private readonly IConfiguracaoEstabelecimentoRepository _configuracaoRepo;

        public ConfiguracaoEstabelecimentoService(IConfiguracaoEstabelecimentoRepository configuracaoRepo)
        {
            _configuracaoRepo = configuracaoRepo;
        }

        public async Task<ServiceResponse<bool>> AtivarCover()
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();

            config.UsarCover = true;
            var atualizado = await _configuracaoRepo.AtualizarConfiguracaoAsync(config);
            return new ServiceResponse<bool>(atualizado, atualizado ? "Cover ativado com sucesso." : "Erro ao ativar o cover.");
        }


        public async Task<ServiceResponse<bool>> DesativarCover()
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();
            config.UsarCover = false;
            var atualizado = await _configuracaoRepo.AtualizarConfiguracaoAsync(config);
            return new ServiceResponse<bool>(atualizado, atualizado ? "Cover desativado com sucesso." : "Erro ao desativar o cover.");
        }

        public async Task<ServiceResponse<CoverStatusDTO>> ObterStatusCover()
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();
            var dto = new CoverStatusDTO { IsCoverAtivo = config.UsarCover, ValorCover = config.ValorCover };
            return new ServiceResponse<CoverStatusDTO>(true, "Operação bem-sucedida.", dto);
        }


        public async Task<ServiceResponse<bool>> AtualizarValorCover(decimal novoValor)
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();
            config.ValorCover = novoValor;
            var atualizado = await _configuracaoRepo.AtualizarConfiguracaoAsync(config);
            return new ServiceResponse<bool>(atualizado, atualizado ? "Valor do cover atualizado com sucesso." : "Erro ao atualizar o valor do cover.");
        }
    }
}
