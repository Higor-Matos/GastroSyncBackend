using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services;


public class ProdutoService : IProdutoService
{

    private readonly IAppDbContext _context;

    public ProdutoService(IAppDbContext context)
    {
        _context = context;
    }

    public List<Produto> GetProdutos()
    {
        return _context.Produtos!.ToList();
    }

}

