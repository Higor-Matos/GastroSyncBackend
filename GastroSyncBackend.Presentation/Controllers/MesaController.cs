using Autofac.Features.Metadata;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MesaController : ControllerBase
{
    private readonly IMesaService _mesaService;

    public MesaController(IMesaService mesaService)
    {
        _mesaService = mesaService;
    }

    [HttpPost("{numeroMesa}")]
    public async Task<IActionResult> CreateMesa(int numeroMesa)
    {
        try
        {
            var mesa = await _mesaService.CreateMesaAsync(numeroMesa);
            return this.ApiResponse<MesaEntity>(true, "Mesa criada com sucesso.", mesa);
        }
        catch (Exception ex)
        {
            return this.ApiResponse<MesaEntity>(false, ex.Message, null!);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var mesas = _mesaService.GetAllMesas();
        return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesas);
    }


}