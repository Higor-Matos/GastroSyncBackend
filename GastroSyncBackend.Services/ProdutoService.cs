using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Repository.Interfaces;

namespace GastroSyncBackend.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public List<Produto> GetAllProdutos() => _produtoRepository.GetAllProdutos();

    public Produto GetProdutoById(int id) => _produtoRepository.GetProdutoById(id);
}