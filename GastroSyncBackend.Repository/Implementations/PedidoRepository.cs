﻿using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Repository.Implementations;

public class PedidoRepository : IPedidoRepository
{
    private readonly IAppDbContext _dbContext;
    private readonly ILogger<PedidoRepository> _logger;

    public PedidoRepository(IAppDbContext dbContext, ILogger<PedidoRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> AdicionarPedidoIndividual(int mesaId, int consumidorId, int produtoId, int quantidade)
    {
        try
        {
            var (mesa, consumidor, produto) = await GetEntitiesAsync(mesaId, consumidorId, produtoId);
            if (mesa == null || consumidor == null || produto == null) return false;

            AdicionarPedido(consumidor, produtoId, produto.Preco, quantidade);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Pedido individual adicionado com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar pedido individual.");
            throw;
        }
    }

    public async Task<bool> AdicionarPedidoDividido(int mesaId, int[] consumidoresIds, int produtoId, int quantidade)
    {
        try
        {
            var mesa = await GetMesaByNumeroAsync(mesaId);
            var produto = await _dbContext.Produtos!.FindAsync(produtoId);
            if (mesa == null || produto == null) return false;

            var valorDividido = (produto.Preco * quantidade) / consumidoresIds.Length;

            foreach (var id in consumidoresIds)
            {
                var consumidor = mesa.Consumidores?.FirstOrDefault(c => c.Id == id);
                if (consumidor == null) continue;

                var pedido = AdicionarPedido(consumidor, produtoId, valorDividido, 1);

                await _dbContext.SaveChangesAsync();

                var divisao = new DivisaoProdutoEntity
                {
                    ConsumidorId = id,
                    PedidoId = pedido.Id,
                    ValorDividido = valorDividido,
                    NomeProduto = produto.Nome,
                    QuantidadeProduto = quantidade,
                    TotalDivisoes = consumidoresIds.Length
                };

                pedido.Divisoes?.Add(divisao);
                _dbContext.DivisoesProdutos!.Add(divisao);
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Pedido dividido adicionado com sucesso.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar pedido dividido.");
            throw;
        }
    }

    public async Task<PedidoEntity?> ObterPedidoPorId(int pedidoId)
    {
        return await _dbContext.Pedidos!.FindAsync(pedidoId);
    }


    private PedidoEntity AdicionarPedido(ConsumidorEntity consumidor, int produtoId, decimal preco, int quantidade)
    {
        if (!consumidor.Id.HasValue) return null!;

        var pedido = CreatePedido(consumidor.Id.Value, produtoId, quantidade);
        UpdateTotalConsumido(consumidor, preco, quantidade);

        consumidor.Pedidos?.Add(pedido);
        _dbContext.Pedidos!.Add(pedido);

        return pedido;
    }

    private async Task<(MesaEntity?, ConsumidorEntity?, ProdutoEntity?)> GetEntitiesAsync(int mesaId, int consumidorId, int produtoId)
    {
        var mesa = await GetMesaByNumeroAsync(mesaId);
        var consumidor = mesa?.Consumidores?.FirstOrDefault(c => c.Id == consumidorId);
        var produto = await _dbContext.Produtos!.FindAsync(produtoId);

        return (mesa, consumidor, produto);
    }

    private async Task<MesaEntity?> GetMesaByNumeroAsync(int mesaNumero) =>
        await _dbContext.Mesas!
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);

    private static PedidoEntity CreatePedido(int consumidorId, int produtoId, int quantidade) =>
        new() { ConsumidorId = consumidorId, ProdutoId = produtoId, Quantidade = quantidade };

    private static void UpdateTotalConsumido(ConsumidorEntity consumidor, decimal precoProduto, int quantidade)
    {
        var total = precoProduto * quantidade;
        consumidor.TotalConsumido += total;
        consumidor.Mesa!.TotalConsumidoMesa += total;
    }
}