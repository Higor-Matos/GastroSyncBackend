using GastroSyncBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[Route("produto")]
public class ProdutoController : Controller
{
    private readonly ProdutoService _produtoService;

    public ProdutoController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("lista")]
    public IActionResult ListaProdutos()
    {
        var produtos = _produtoService.GetAllProdutos();
        return View(produtos);
    }
}