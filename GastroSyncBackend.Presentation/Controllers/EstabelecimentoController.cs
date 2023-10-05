using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstabelecimentoController : ControllerBase
{
    private readonly IConfiguracaoEstabelecimentoService _configuracaoEstabelecimentoService;

    public EstabelecimentoController(IConfiguracaoEstabelecimentoService configuracaoEstabelecimentoService)
    {
        _configuracaoEstabelecimentoService = configuracaoEstabelecimentoService;
    }

    [HttpPost("AtivarCover")]
    public async Task<IActionResult> AtivarCover()
    {
        var result = await _configuracaoEstabelecimentoService.AtivarCover();
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }

    [HttpPost("DesativarCover")]
    public async Task<IActionResult> DesativarCover()
    {
        var result = await _configuracaoEstabelecimentoService.DesativarCover();
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }

    [HttpGet("StatusCover")]
    public async Task<IActionResult> StatusCover()
    {
        var result = await _configuracaoEstabelecimentoService.ObterStatusCover();

        if (!result.Success)
            return this.ApiResponse<bool>(result.Success, result.Message, result.Data);

        var coverStatusDto = new CoverStatusDTO { IsCoverAtivo = result.Data };

        return this.ApiResponse<CoverStatusDTO>(result.Success, result.Message, coverStatusDto);
    }


    [HttpPost("AtualizarValorCover/{novoValor:decimal}")]
    public async Task<IActionResult> AtualizarValorCover(decimal novoValor)
    {
        var result = await _configuracaoEstabelecimentoService.AtualizarValorCover(novoValor);
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }
}
