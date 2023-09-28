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

    [HttpGet("ObterTodasAsMesas")]
    public async Task<IActionResult> ObterTodasAsMesas()
    {
        var result = await _mesaService.ObterTodasAsMesas();
        if (!result.Success) return this.ApiResponse<object>(result.Success, result.Message, null!);
        var mesasDto = _mapper.Map<List<MesaDTO>>(result.Data);
        return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesasDto);
    }

    [HttpGet("ObterMesaPorNumero/{numeroMesa:int}")]
    public async Task<IActionResult> ObterPorNumeroMesa(int numeroMesa)
    {
        var result = await _mesaService.ObterMesaPorNumero(numeroMesa);
        return this.ApiResponse(result.Success, result.Message, result.Data != null ? _mapper.Map<MesaDTO>(result.Data) : null);
    }


    [HttpGet("{mesaNumero:int}/ConsumoTotalMesa")]
    public async Task<IActionResult> ObterConsumoTotalMesa(int mesaNumero)
    {
        var result = await _mesaService.ObterConsumoTotalMesa(mesaNumero);
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }

    [HttpDelete("RemoveMesaPeloNumero/{mesaNumber:int}")]
    public async Task<IActionResult> RemoveMesaPeloNumero(int mesaNumber)
    {
        var result = await _mesaService.RemoveMesaPeloNumero(mesaNumber);
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }


    [HttpDelete("RemoveTodasMesas")]
    public async Task<IActionResult> RemoveTodasMesasEReiniciaId()
    {
        var result = await _mesaService.RemoveTodasMesasEReiniciaId();
        return this.ApiResponse(result.Success, result.Message, result.Data);
    }



    [HttpPost("CriarMesa")]
    public async Task<IActionResult> CriarMesa([FromBody] MesaCreateRequest request)
    {
        try
        {
            var result = await _mesaService.CriarMesa(request.NumeroMesa, request.Local!);
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception)
        {
            return this.ApiResponse<MesaEntity>(false, "Operação concluída", null);
        }
    }

}