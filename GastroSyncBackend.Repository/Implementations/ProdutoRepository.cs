using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Repository.Implementations;

public class ProdutoRepository : IProdutoRepository
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<ProdutoRepository> _logger;

    public ProdutoRepository(IAppDbContext dbContext, ILogger<ProdutoRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<ProdutoEntity>> ObterTodosOsProdutos()
    {
        try
        {
            return await _dbContext.Produtos!.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter todos os produtos.");
            throw;
        }
    }
}