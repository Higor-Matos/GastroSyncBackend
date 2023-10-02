﻿using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class PedidoRepository : IPedidoRepository
{
    private readonly IAppDbContext _dbContext;

    public PedidoRepository(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> AdicionarPedidoIndividual(int mesaId, int consumidorId, int produtoId, int quantidade)
    {
        var mesa = await GetMesaByNumeroAsync(mesaId);
        var consumidor = mesa?.Consumidores?.FirstOrDefault(c => c.Id == consumidorId);
        var produto = await _dbContext.Produtos!.FindAsync(produtoId);

        if (mesa == null || consumidor == null || produto == null) return false;
        var pedido = CreatePedido(consumidorId, produtoId, quantidade);
        UpdateTotalConsumido(consumidor, mesa, produto.Preco, quantidade);

        consumidor.Pedidos?.Add(pedido);

        await _dbContext.SaveChangesAsync();

        return true;

    }

    public async Task<bool> AdicionarPedidoDividido(int mesaId, int[] consumidoresIds, int produtoId, int quantidade)
    {
        var mesa = await GetMesaByNumeroAsync(mesaId);
        var produto = await _dbContext.Produtos!.FindAsync(produtoId);
        if (mesa == null || produto == null) return false;

        var totalPreco = produto.Preco * quantidade;
        var valorDividido = totalPreco / consumidoresIds.Length;

        foreach (var consumidorId in consumidoresIds)
        {
            var consumidor = mesa?.Consumidores?.FirstOrDefault(c => c.Id == consumidorId);
            if (consumidor == null) continue;

            var pedido = CreatePedido(consumidorId, produtoId, quantidade);
            consumidor.Pedidos?.Add(pedido);

            var divisao = new DivisaoProdutoEntity
            {
                ConsumidorId = consumidorId,
                PedidoId = pedido.Id,
                ValorDividido = valorDividido
            };
            pedido.Divisoes?.Add(divisao);

            UpdateTotalConsumido(consumidor, mesa!, valorDividido, 1);
        }

        await _dbContext.SaveChangesAsync();

        return true;
    }



    private async Task<MesaEntity?> GetMesaByNumeroAsync(int mesaNumero) =>
        await _dbContext.Mesas!
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);

    private static PedidoEntity CreatePedido(int consumidorId, int produtoId, int quantidade)
    {
        return new PedidoEntity
        {
            ConsumidorId = consumidorId,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };
    }

    private static void UpdateTotalConsumido(ConsumidorEntity consumidor, MesaEntity mesa, decimal precoProduto, int quantidade)
    {
        var total = precoProduto * quantidade;
        consumidor.TotalConsumido += total;
        mesa.TotalConsumido += total;
    }
}