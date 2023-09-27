using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MesaController : ControllerBase
{
    private readonly IMesaService _mesaService;
    private readonly IMapper _mapper;

    public MesaController(IMesaService mesaService, IMapper mapper)
    {
        _mesaService = mesaService;
        _mapper = mapper;
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
    public async Task<IActionResult> ObterTodas()
    {
        var result = await _mesaService.ObterTodasAsMesasAsync();
        if (result.Success)
        {
            var mesasDto = _mapper.Map<List<MesaDTO>>(result.Data);
            return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesasDto);
        }
        return this.ApiResponse<object>(result.Success, result.Message, null!);
    }


    [HttpGet("numero/{numeroMesa}")]
    public async Task<IActionResult> ObterPorNumeroMesa(int numeroMesa)
    {
        var result = await _mesaService.ObterMesaPorNumeroAsync(numeroMesa);
        if (result.Success)
        {
            var mesaDto = _mapper.Map<MesaDTO>(result.Data);
            return this.ApiResponse(true, "Mesa recuperada com sucesso", mesaDto);
        }
        return this.ApiResponse<object>(result.Success, result.Message, null!);
    }
    

    [HttpDelete("{mesaNumber}")]
    public async Task<IActionResult> DeleteByMesaNumber(int mesaNumber)
    {
        var isRemoved = await _mesaService.RemoveMesaByMesaNumber(mesaNumber);
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
            var result = await _mesaService.AddConsumidoresAsync(mesaId, consumidores);
            return result ? this.ApiResponse<MesaEntity>(true, "Consumidores adicionados com sucesso.", null!) : this.ApiResponse<MesaEntity>(false, "Falha ao adicionar consumidores.", null!);
        }
        catch (Exception ex)
        {
            return this.ApiResponse<MesaEntity>(false, ex.Message, null!);
        }
    }


}