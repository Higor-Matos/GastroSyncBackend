using GastroSyncBackend.Domain;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly ILogger<ProdutoController> _logger;

    public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
    {
        _produtoService = produtoService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProdutos()
    {
        try
        {
            var produtos = _produtoService.GetProdutos();
            return this.ApiResponse(true, "Produtos obtidos com sucesso.", produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError("Ocorreu uma exceção ao tentar obter produtos: {Message}", ex.Message);
            return this.ApiResponse(false, "Falha ao obter produtos.", (string)null!);
        }
    }

    [HttpPost("ByCategoria")]
    public IActionResult GetProdutosByCategoria([FromBody] CategoriaRequest request)
    {
        try
        {
            var produtos = _produtoService.GetProdutosByCategoria(request.Categoria!);
            return this.ApiResponse(true, "Produtos recuperados com sucesso.", produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao recuperar produtos: {ErrorMessage}", ex.Message);
            return this.ApiResponse(false, "Erro interno do servidor.", string.Empty);
        }
    }
}