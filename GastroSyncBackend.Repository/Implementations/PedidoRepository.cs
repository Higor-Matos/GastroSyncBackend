using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class PedidoRepository : IPedidoRepository
{
    private readonly IAppDbContext _dbContext;

    public PedidoRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> AdicionarPedidoConsumidorMesa(int mesaId, int consumidorId, int produtoId, int quantidade)
    {
        var mesa = await _dbContext.Mesas!.Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaId);

        var consumidor = mesa?.Consumidores?.FirstOrDefault(c => c.Id == consumidorId);

        var produto = await _dbContext.Produtos!.FindAsync(produtoId);

        if (mesa == null || consumidor == null || produto == null)
        {
            return false;
        }

        var pedido = new PedidoEntity
        {
            ConsumidorId = consumidorId,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };

        consumidor.TotalConsumido += produto.Preco * quantidade;
        mesa.TotalConsumido += produto.Preco * quantidade;

        consumidor.Pedidos?.Add(pedido);

        await _dbContext.SaveChangesAsync();

        return true;
    }

}