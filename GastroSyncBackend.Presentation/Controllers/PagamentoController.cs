using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly IPagamentoService _pagamentoService;

    public PagamentoController(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    [HttpPost("RealizarPagamento")]
    public async Task<IActionResult> RealizarPagamento(int consumidorId, decimal valor)
    {
        var result = await _pagamentoService.RealizarPagamento(consumidorId, valor);

        return result.Success ? this.ApiResponse(true, "Pagamento realizado com sucesso.", result.Data) : this.ApiResponse(false, "Falha ao realizar o pagamento.", result.Data);
    }
}