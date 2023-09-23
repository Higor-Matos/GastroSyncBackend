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

    [HttpPost("criar/{numeroMesa}")]
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

    [HttpGet("todas")]
    public IActionResult GetAll()
    {
        var mesas = _mesaService.GetAllMesas();
        return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesas);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var mesa = _mesaService.GetMesaById(id);
        return mesa != null ? this.ApiResponse(true, "Mesa recuperada com sucesso", mesa) : this.ApiResponse<MesaEntity>(false, "Mesa não encontrada", null!);
    }

}