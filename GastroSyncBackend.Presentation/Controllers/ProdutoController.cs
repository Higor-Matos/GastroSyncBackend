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
        const string logMessageTemplate = "Ocorreu uma exceção ao tentar obter produtos: {Message}";

        try
        {
            var produtos = _produtoService.GetProdutos();
            return this.ApiResponse(true, "Produtos obtidos com sucesso.", produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(logMessageTemplate, ex.Message);
            return this.ApiResponse(false, "Falha ao obter produtos.", (string)null!);
        }
    }
}