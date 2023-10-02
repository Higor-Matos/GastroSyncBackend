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

    [HttpGet("RecuperarTodosProdutos")]
    public async Task<IActionResult> ObterTodosOsProdutos()
    {
        var result = await _produtoService.GetProdutosAsync();
        return this.ApiResponse(result.Success, result.Message, _mapper.Map<List<ProdutoDTO>>(result.Data));
    }
}