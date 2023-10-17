using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Repository.Implementations;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<PagamentoRepository> _logger;

    public PagamentoRepository(IAppDbContext dbContext, ILogger<PagamentoRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagamentoEntity> CriarPagamento(PagamentoEntity pagamento)
    {
        try
        {
            await _dbContext.Pagamentos!.AddAsync(pagamento);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Pagamento criado com sucesso.");
            return pagamento;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar pagamento.");
            throw;
        }
    }

    public async Task<IEnumerable<PagamentoEntity>> ObterPagamentosPorConsumidor(int consumidorId)
    {
        try
        {
            return await _dbContext.Pagamentos!
                .Where(p => p.ConsumidorId == consumidorId)
                .AsSplitQuery()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter pagamentos por consumidor.");
            throw;
        }
    }

    public async Task<List<PagamentoDetalhadoDto>> ObterPagamentosDetalhados()
    {
        return await _dbContext.Pagamentos
            .Include(p => p.Consumidor)
            .ThenInclude(c => c.Pedidos)
            .Select(p => new PagamentoDetalhadoDto
            {
                Id = p.Id,
                ConsumidorId = p.ConsumidorId,
                ConsumidorNome = p.Consumidor.Nome,
                ValorPago = p.ValorPago,
                DataPagamento = p.DataPagamento,
                PedidosPagos = p.Consumidor.Pedidos.Select(pe => pe.Id.ToString()).ToList()
            })
            .ToListAsync();
    }

}