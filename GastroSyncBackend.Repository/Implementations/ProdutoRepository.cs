using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class ProdutoRepository : IProdutoRepository
{

    private readonly IAppDbContext _dbContext;

    public ProdutoRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ProdutoEntity>> GetProdutosAsync()
    {
        return await _dbContext.Produtos!.ToListAsync();
    }

    public async Task<List<ProdutoEntity>> GetProdutosByCategoriaAsync(string categoria)
    {
        return await _dbContext.Produtos!.Where(p => p.Categoria == categoria).ToListAsync();
    }

}