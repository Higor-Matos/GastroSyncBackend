using AutoMapper;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ConsumidorController : ControllerBase
{

    private readonly IConsumidorService _consumidorService;
    private readonly IMapper _mapper;

    public ConsumidorController(IConsumidorService consumidorService, IMapper mapper)
    {
        _consumidorService = consumidorService;
        _mapper = mapper;
    }

    [HttpPost("{mesaId:int}/AdicionarConsumidoresMesa")]
    public async Task<IActionResult> AdicionarConsumidoresMesa(int mesaId, [FromBody] List<string> consumidores)
    {
        try
        {
            var result = await _consumidorService.AdicionarConsumidoresMesa(mesaId, consumidores);
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception)
        {
            return this.ApiResponse(false, "Operação concluída", false);
        }
    }

}