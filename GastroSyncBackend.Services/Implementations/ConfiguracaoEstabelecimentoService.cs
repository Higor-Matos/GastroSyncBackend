using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repositories.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Services.Implementations;

public class ConfiguracaoEstabelecimentoService : IConfiguracaoEstabelecimentoService
{
    private readonly IConfiguracaoEstabelecimentoRepository _configuracaoRepo;
    private readonly ILogger<ConfiguracaoEstabelecimentoService> _logger;

    public ConfiguracaoEstabelecimentoService(IConfiguracaoEstabelecimentoRepository configuracaoRepo, ILogger<ConfiguracaoEstabelecimentoService> logger)
    {
        _configuracaoRepo = configuracaoRepo;
        _logger = logger;
    }

    public async Task<ServiceResponse<bool>> AtivarCover()
    {
        try
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();

            config.UsarCover = true;
            var atualizado = await _configuracaoRepo.AtualizarConfiguracaoAsync(config);

            _logger.LogInformation("Cover ativado com sucesso.");

            return new ServiceResponse<bool>(atualizado, atualizado ? "Cover ativado com sucesso." : "Erro ao ativar o cover.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao ativar o cover.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao ativar o cover.");
        }
    }

    public async Task<ServiceResponse<bool>> DesativarCover()
    {
        try
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();

            config.UsarCover = false;
            var atualizado = await _configuracaoRepo.AtualizarConfiguracaoAsync(config);

            _logger.LogInformation("Cover desativado com sucesso.");

            return new ServiceResponse<bool>(atualizado, atualizado ? "Cover desativado com sucesso." : "Erro ao desativar o cover.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao desativar o cover.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao desativar o cover.");
        }
    }

    public async Task<ServiceResponse<CoverStatusDTO>> ObterStatusCover()
    {
        try
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();
            var dto = new CoverStatusDTO { IsCoverAtivo = config.UsarCover, ValorCover = config.ValorCover };

            _logger.LogInformation("Status do Cover obtido com sucesso.");

            return new ServiceResponse<CoverStatusDTO>(true, "Operação bem-sucedida.", dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter o status do Cover.");
            return new ServiceResponse<CoverStatusDTO>(false, "Ocorreu um erro ao obter o status do Cover.");
        }
    }

    public async Task<ServiceResponse<bool>> AtualizarValorCover(decimal novoValor)
    {
        try
        {
            var config = await _configuracaoRepo.ObterConfiguracaoAsync();

            config.ValorCover = novoValor;
            var atualizado = await _configuracaoRepo.AtualizarConfiguracaoAsync(config);

            _logger.LogInformation("Valor do Cover atualizado com sucesso.");

            return new ServiceResponse<bool>(atualizado, atualizado ? "Valor do Cover atualizado com sucesso." : "Erro ao atualizar o valor do Cover.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar o valor do Cover.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao atualizar o valor do Cover.");
        }
    }
}