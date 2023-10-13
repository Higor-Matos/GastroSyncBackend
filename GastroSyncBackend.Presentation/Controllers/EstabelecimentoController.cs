using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstabelecimentoController : ControllerBase
{
    private readonly IConfiguracaoEstabelecimentoService _configuracaoEstabelecimentoService;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public EstabelecimentoController(IConfiguracaoEstabelecimentoService configuracaoEstabelecimentoService)
    {
        _configuracaoEstabelecimentoService = configuracaoEstabelecimentoService;
    }

    [HttpPost("AtivarCover")]
    public async Task<IActionResult> AtivarCover()
    {
        try
        {
            var result = await _configuracaoEstabelecimentoService.AtivarCover();

            _logger.Info("Método AtivarCover executado com sucesso.");

            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método AtivarCover.");
            return this.ApiResponse<bool>(false, "Ocorreu um erro ao executar a operação.", false);
        }
    }

    [HttpPost("DesativarCover")]
    public async Task<IActionResult> DesativarCover()
    {
        try
        {
            var result = await _configuracaoEstabelecimentoService.DesativarCover();

            _logger.Info("Método DesativarCover executado com sucesso.");

            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método DesativarCover.");
            return this.ApiResponse<bool>(false, "Ocorreu um erro ao executar a operação.", false);
        }
    }

    [HttpGet("StatusCover")]
    public async Task<IActionResult> StatusCover()
    {
        try
        {
            var result = await _configuracaoEstabelecimentoService.ObterStatusCover();

            _logger.Info("Método StatusCover executado com sucesso.");

            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método StatusCover.");
            return this.ApiResponse<bool>(false, "Ocorreu um erro ao executar a operação.", false);
        }
    }

    [HttpPost("AtualizarValorCover/{novoValor:decimal}")]
    public async Task<IActionResult> AtualizarValorCover(decimal novoValor)
    {
        try
        {
            var result = await _configuracaoEstabelecimentoService.AtualizarValorCover(novoValor);

            _logger.Info("Método AtualizarValorCover executado com sucesso.");

            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método AtualizarValorCover.");
            return this.ApiResponse<bool>(false, "Ocorreu um erro ao executar a operação.", false);
        }
    }
}