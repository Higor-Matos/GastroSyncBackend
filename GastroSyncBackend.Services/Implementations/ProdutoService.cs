using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services;


public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository) => _produtoRepository = produtoRepository;

    public async Task<ServiceResponse<List<ProdutoEntity>>> GetProdutosAsync()
    {
        var produtos = await _produtoRepository.GetProdutosAsync();
        return new ServiceResponse<List<ProdutoEntity>>(true, "Operação concluída", produtos);
    }
}