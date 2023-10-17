using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly IPagamentoService _pagamentoService;
    private readonly IMapper _mapper;

    public PagamentoController(IPagamentoService pagamentoService, IMapper mapper)
    {
        _pagamentoService = pagamentoService;
        _mapper = mapper;
    }

    [HttpPost("RealizarPagamento")]
    public async Task<IActionResult> RealizarPagamento(int consumidorId, decimal valor)
    {
        var result = await _pagamentoService.RealizarPagamento(consumidorId, valor);

        return result.Success ? this.ApiResponse(true, "Pagamento realizado com sucesso.", result.Data) : this.ApiResponse(false, "Falha ao realizar o pagamento.", result.Data);
    }

    [HttpGet("ObterPagamentosDetalhados")]
    public async Task<IActionResult> ObterPagamentosDetalhados()
    {
        var pagamentos = await _pagamentoService.ObterPagamentosDetalhados();

        var pagamentosDto = _mapper.Map<List<PagamentoDetalhadoDto>>(pagamentos);

        return Ok(pagamentosDto);
    }

}