using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MesaController : ControllerBase
{
    private readonly IMesaService _mesaService;
    private readonly IMapper _mapper;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public MesaController(IMesaService mesaService, IMapper mapper)
    {
        _mesaService = mesaService;
        _mapper = mapper;
    }

    [HttpGet("ObterTodasAsMesas")]
    public async Task<IActionResult> ObterTodasAsMesas()
    {
        try
        {
            var result = await _mesaService.ObterTodasAsMesas();
            if (!result.Success)
            {
                _logger.Warn("Falha ao obter todas as mesas: " + result.Message);
                return this.ApiResponse<object>(result.Success, result.Message, null!);
            }
            var mesasDto = _mapper.Map<List<MesaDTO>>(result.Data);
            _logger.Info("Método ObterTodasAsMesas executado com sucesso.");
            return this.ApiResponse(true, "Mesas recuperadas com sucesso", mesasDto);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método ObterTodasAsMesas.");
            return this.ApiResponse<object>(false, "Ocorreu um erro ao executar a operação.", null!);
        }
    }

    [HttpDelete("RemoveMesaPeloNumero/{mesaNumber:int}")]
    public async Task<IActionResult> RemoveMesaPeloNumero(int mesaNumber)
    {
        try
        {
            var result = await _mesaService.RemoveMesaPeloNumero(mesaNumber);
            _logger.Info($"Método RemoveMesaPeloNumero executado com sucesso para a mesa {mesaNumber}.");
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Erro ao executar o método RemoveMesaPeloNumero para a mesa {mesaNumber}.");
            return this.ApiResponse<bool>(false, "Operação concluída", false);
        }
    }

    [HttpDelete("RemoveTodasMesas")]
    public async Task<IActionResult> RemoveTodasMesasEReiniciaId()
    {
        try
        {
            var result = await _mesaService.RemoveTodasMesasEReiniciaId();
            _logger.Info("Método RemoveTodasMesasEReiniciaId executado com sucesso.");
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método RemoveTodasMesasEReiniciaId.");
            return this.ApiResponse<MesaEntity>(false, "Operação concluída", null);
        }
    }

    [HttpPost("CriarMesa")]
    public async Task<IActionResult> CriarMesa([FromQuery] int numeromesa, [FromQuery] string local)
    {
        try
        {
            var result = await _mesaService.CriarMesa(numeromesa, local);
            _logger.Info("Método CriarMesa executado com sucesso.");
            return this.ApiResponse(result.Success, result.Message, result.Data);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método CriarMesa.");
            return this.ApiResponse<MesaEntity>(false, "Operação concluída", null);
        }
    }


}