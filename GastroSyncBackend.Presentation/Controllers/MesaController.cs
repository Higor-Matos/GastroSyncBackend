using GastroSyncBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[Route("mesa")]
public class MesaController : Controller
{
    private readonly MesaService _mesaService;
    private readonly ProdutoService _produtoService;

    public MesaController(MesaService mesaService, ProdutoService produtoService)
    {
        _mesaService = mesaService;
        _produtoService = produtoService;
    }

    [HttpGet("{id}")]
    public IActionResult DetalhesMesa(int id)
    {
        var mesa = _mesaService.GetMesaById(id);
        return View(mesa);
    }

    [HttpPost("{id}/adicionar-produto")]
    public IActionResult AdicionarProduto(int id, int produtoId)
    {
        var produto = _produtoService.GetProdutoById(produtoId);
        _mesaService.AddProdutoToMesa(id, produto);
        return RedirectToAction($"DetalhesMesa", new { id });
    }
}

