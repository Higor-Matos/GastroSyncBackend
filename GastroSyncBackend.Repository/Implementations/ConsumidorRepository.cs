using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class ConsumidorRepository : IConsumidorRepository
{
    private readonly IAppDbContext _dbContext;

    public ConsumidorRepository(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores)
    {
        var mesa = await GetMesaByNumeroAsync(mesaId);
        if (mesa == null) return false;

        var consumidorEntities = consumidores.Select(nome => new ConsumidorEntity { Nome = nome, MesaId = mesaId }).ToList();
        mesa.Consumidores!.AddRange(consumidorEntities);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<ConsumidorEntity>?> ObterConsumidoresMesa(int mesaNumero)
    {
        var mesa = await GetMesaByNumeroAsync(mesaNumero);
        return mesa?.Consumidores;
    }

    public async Task<ConsumoIndividualDTO?> ObterConsumoIndividualMesa(int mesaNumero, int consumidorId)
    {
        var consumidor = await _dbContext.Consumidores!
            .Include(c => c.Pedidos)!
            .ThenInclude(p => p.Produto)
            .Include(c => c.Pedidos)!
            .ThenInclude(p => p.Divisoes)
            .Where(c => c.MesaId.HasValue && c.Mesa!.NumeroMesa == mesaNumero)
            .FirstOrDefaultAsync(c => c.Id == consumidorId);

        if (consumidor == null) return null;
        {
            var consumoIndividualDto = new ConsumoIndividualDTO
            {
                ConsumidorId = consumidor.Id!.Value,
                ConsumidorNome = consumidor.Nome,
                TotalIndividual = consumidor.TotalConsumido,
                PedidosConsumidos = consumidor.Pedidos!.Select(p => new PedidoDTO
                {
                    Id = p.Id,
                    ConsumidorId = p.ConsumidorId,
                    ProdutoId = p.ProdutoId,
                    Quantidade = p.Quantidade,
                    Divisoes = p.Divisoes!.Select(d => new DivisaoProdutoDTO
                    {
                        ConsumidorId = consumidor.Id ?? 0,
                        ValorDividido = d.ValorDividido
                    }).ToList()
                }).ToList()
            };


            return consumoIndividualDto;
        }

    }

    private async Task<MesaEntity?> GetMesaByNumeroAsync(int mesaNumero) =>
        await _dbContext.Mesas!
            .Include(m => m.Consumidores)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);
}