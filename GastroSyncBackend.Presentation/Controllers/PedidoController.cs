using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;


[ApiController]
[Route("api/[controller]/{mesaId:int}")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost("Consumidores/{consumidorId:int}")]
    public async Task<IActionResult> Add(int mesaId, int consumidorId, [FromBody] AddPedidoRequest request)
    {
        var result = await _pedidoService.AdicionarPedidoIndividual(mesaId, consumidorId, request.ProdutoId, request.Quantidade);
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }

    [HttpPost("Dividido")]
    public async Task<IActionResult> AddDividido(int mesaId, [FromBody] AddPedidoDivididoRequest request)
    {
        var result = await _pedidoService.AdicionarPedidoDividido(mesaId, request.ConsumidoresIds, request.ProdutoId, request.Quantidade);
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }

}