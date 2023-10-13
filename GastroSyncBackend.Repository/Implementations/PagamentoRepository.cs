using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly IAppDbContext _dbContext;

    public PagamentoRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagamentoEntity> CriarPagamento(PagamentoEntity pagamento)
    {
        await _dbContext.Pagamentos.AddAsync(pagamento);
        await _dbContext.SaveChangesAsync();
        return pagamento;
    }

    public async Task<IEnumerable<PagamentoEntity>> ObterPagamentosPorConsumidor(int consumidorId)
    {
        return await _dbContext.Pagamentos
            .Where(p => p.ConsumidorId == consumidorId)
            .ToListAsync();
    }
}