using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost("{mesaId:int}/Consumidores/{consumidorId:int}/AdicionarPedidoConsumidorMesa")]
    public async Task<IActionResult> AdicionarPedidoConsumidorMesa(int mesaId, int consumidorId, [FromBody] AddPedidoRequest request)
    {
        var result = await _pedidoService.AdicionarPedidoConsumidorMesa(mesaId, consumidorId, request.ProdutoId, request.Quantidade);
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }
}