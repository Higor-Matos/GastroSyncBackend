using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public ProdutoController(IProdutoService produtoService, IMapper mapper)
    {
        _produtoService = produtoService;
        _mapper = mapper;
    }

    [HttpGet("RecuperarTodosProdutos")]
    public async Task<IActionResult> ObterTodosOsProdutos()
    {
        try
        {
            var result = await _produtoService.ObterTodosOsProdutos();
            _logger.Info("Método ObterTodosOsProdutos executado com sucesso.");
            return this.ApiResponse(result.Success, result.Message, _mapper.Map<List<ProdutoDTO>>(result.Data));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao executar o método ObterTodosOsProdutos.");
            return this.ApiResponse<object>(false, "Ocorreu um erro ao executar a operação.", null!);
        }
    }
}