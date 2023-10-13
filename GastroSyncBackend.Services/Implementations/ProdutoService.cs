using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Services.Implementations;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogger<ProdutoService> _logger;

    public ProdutoService(IProdutoRepository produtoRepository, ILogger<ProdutoService> logger)
    {
        _produtoRepository = produtoRepository;
        _logger = logger;
    }

    public async Task<ServiceResponse<List<ProdutoEntity>>> ObterTodosOsProdutos()
    {
        try
        {
            var produtos = await _produtoRepository.ObterTodosOsProdutos();
            _logger.LogInformation("Produtos obtidos com sucesso.");
            return new ServiceResponse<List<ProdutoEntity>>(true, "Operação concluída", produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter todos os produtos.");
            return new ServiceResponse<List<ProdutoEntity>>(false, "Ocorreu um erro ao obter todos os produtos.");
        }
    }
}