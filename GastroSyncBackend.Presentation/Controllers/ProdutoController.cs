using AutoMapper;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Request;
using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;

    public ProdutoController(IProdutoService produtoService, IMapper mapper)
    {
        _produtoService = produtoService;
        _mapper = mapper;
    }

    [HttpGet("todos")]
    public async Task<IActionResult> ObterTodosOsProdutos()
    {
        var produtos = await _produtoService.GetProdutosAsync();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return this.ApiResponse(true, "Produtos obtidos com sucesso.", produtosDto);
    }

    [HttpPost("porCategoria")]
    public async Task<IActionResult> ObterProdutosPorCategoria([FromBody] CategoriaRequest request)
    {
        var produtos = await _produtoService.GetProdutosByCategoriaAsync(request.Categoria!);
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return this.ApiResponse(true, "Produtos recuperados com sucesso.", produtosDto);
    }
}