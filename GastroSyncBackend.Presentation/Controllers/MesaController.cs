using GastroSyncBackend.Domain;
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

    [HttpPost("criar")]
    public async Task<IActionResult> CreateMesa([FromBody] MesaCreateRequest request)
    {
        try
        {
            // Verifica se a mesa com o número dado já existe
            if (await _mesaService.MesaExisteAsync(request.NumeroMesa))
            {
                return this.ApiResponse<MesaEntity>(false, "Número de mesa já existe.", null!);
            }

            // Cria a nova mesa se não existir uma com o mesmo número
            var mesa = await _mesaService.CreateMesaAsync(request.NumeroMesa, request.Local!);
            return this.ApiResponse(true, "Mesa criada com sucesso.", mesa);
        }
        catch (Exception ex)
        {
            return this.ApiResponse<MesaEntity>(false, ex.Message, null!);
        }
    }



    [HttpGet("todas")]
    public async Task<IActionResult> GetAll()
    {
        var mesas = await _mesaService.GetAllMesas();
        return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mesa = await _mesaService.GetMesaById(id);
        return mesa != null ? this.ApiResponse(true, "Mesa recuperada com sucesso", mesa) : this.ApiResponse<MesaEntity>(false, "Mesa não encontrada", null!);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var isRemoved = await _mesaService.RemoveMesaById(id);
        return isRemoved ? this.ApiResponse<MesaEntity>(true, "Mesa removida com sucesso", null!) : this.ApiResponse<MesaEntity>(false, "Mesa não encontrada", null!);
    }


    [HttpDelete("RemoveAll")]
    public async Task<IActionResult> RemoveAll()
    {
        await _mesaService.RemoveAllMesasAndResetId();
        return this.ApiResponse<MesaEntity>(true, "Todas as mesas foram removidas", null!);
    }

}