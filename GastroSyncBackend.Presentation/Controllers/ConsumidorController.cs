using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
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

    [HttpGet("{mesaNumero:int}/ObterConsumidoresMesa")]
    public async Task<IActionResult> ObterConsumidoresMesa(int mesaNumero)
    {
        var result = await _consumidorService.ObterConsumidoresMesa(mesaNumero);
        return this.ApiResponse(result.Success, result.Message, _mapper.Map<List<ConsumidorDTO>>(result.Data));
    }

    [HttpGet("{mesaNumero:int}/Consumidores/{consumidorId:int}/ConsumoIndividual")]
    public async Task<IActionResult> ObterConsumoIndividualMesa(int mesaNumero, int consumidorId)
    {
        var result = await _consumidorService.ObterConsumoIndividualMesa(mesaNumero, consumidorId);
        return this.ApiResponse(result.Success, result.Message, result.Data);
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