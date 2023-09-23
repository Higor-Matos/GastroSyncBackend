using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using GastroSyncBackend.Services.Interfaces.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MesaController : ControllerBase
{
    private readonly IMesaService _mesaService;
    private readonly IMapToDtoService _mapToDtoService;

    public MesaController(IMesaService mesaService, IMapToDtoService mapToDtoService)
    {
        _mesaService = mesaService;
        _mapToDtoService = mapToDtoService;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> CreateMesa([FromBody] MesaCreateRequest request)
    {
        try
        {
            if (await _mesaService.MesaExisteAsync(request.NumeroMesa))
            {
                return this.ApiResponse<MesaEntity>(false, "Número de mesa já existe.", null!);
            }

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
        var mesaDtos = new List<MesaDto>();

        foreach (var mesa in mesas)
        {
            var mesaDto = await _mapToDtoService.MapMesaToDtoAsync(mesa);
            mesaDtos.Add(mesaDto);
        }

        return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesaDtos);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mesa = await _mesaService.GetMesaById(id);
        {
            var mesaDto = await _mapToDtoService.MapMesaToDtoAsync(mesa);
            return this.ApiResponse(true, "Mesa recuperada com sucesso", mesaDto);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var isRemoved = await _mesaService.RemoveMesaById(id);
        return isRemoved
            ? this.ApiResponse<MesaEntity>(true, "Mesa removida com sucesso", null!)
            : this.ApiResponse<MesaEntity>(false, "Mesa não encontrada", null!);
    }


    [HttpDelete("RemoveAll")]
    public async Task<IActionResult> RemoveAll()
    {
        await _mesaService.RemoveAllMesasAndResetId();
        return this.ApiResponse<MesaEntity>(true, "Todas as mesas foram removidas", null!);
    }

    [HttpPost("{mesaId}/add-consumidores")]
    public async Task<IActionResult> AddConsumidores(int mesaId, [FromBody] List<string> consumidores)
    {
        try
        {
            await _mesaService.AddConsumidoresAsync(mesaId, consumidores);
            return this.ApiResponse<MesaEntity>(true, "Consumidores adicionados com sucesso.", null!);
        }
        catch (Exception ex)
        {
            return this.ApiResponse<MesaEntity>(false, ex.Message, null!);
        }
    }

}