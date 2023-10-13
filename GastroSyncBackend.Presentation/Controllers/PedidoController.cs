using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/{mesaId:int}")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost("Consumidores/{consumidorId:int}")]
    public async Task<IActionResult> Add(int mesaId, int consumidorId, [FromBody] AddPedidoRequest request)
    {
        try
        {
            var result = await _pedidoService.AdicionarPedidoIndividual(mesaId, consumidorId, request.ProdutoId, request.Quantidade);
            _logger.Info("Método Add executado com sucesso.");
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método Add.");
            return this.ApiResponse<object>(false, "Ocorreu um erro ao executar a operação.", null!);
        }
    }

    [HttpPost("Dividido")]
    public async Task<IActionResult> AddDividido(int mesaId, [FromBody] AddPedidoDivididoRequest request)
    {
        try
        {
            var result = await _pedidoService.AdicionarPedidoDividido(mesaId, request.ConsumidoresIds, request.ProdutoId, request.Quantidade);
            _logger.Info("Método AddDividido executado com sucesso.");
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método AddDividido.");
            return this.ApiResponse<object>(false, "Ocorreu um erro ao executar a operação.", null!);
        }
    }
}