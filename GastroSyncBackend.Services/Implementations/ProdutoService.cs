using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Services;


public class ProdutoService : IProdutoService
{

    private readonly IAppDbContext _context;

    public ProdutoService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProdutoEntity>> GetProdutosAsync()
    {
        return await _context.Produtos!.ToListAsync();
    }

    public async Task<List<ProdutoEntity>> GetProdutosByCategoriaAsync(string categoria)
    {
        return await _context.Produtos?.Where(p => p.Categoria == categoria).ToListAsync()!;
    }

}