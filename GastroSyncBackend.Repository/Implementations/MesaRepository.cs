using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GastroSyncBackend.Repository.Implementations;

public class MesaRepository : IMesaRepository
{
    private readonly IAppDbContext _dbContext;

    public MesaRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MesaEntity?> CriarMesa(int numeroMesa, string local)
    {
        var mesa = new MesaEntity { NumeroMesa = numeroMesa, Local = local };
        _dbContext.Mesas!.Add(mesa);
        await _dbContext.SaveChangesAsync();
        return mesa;
    }


    public async Task<IEnumerable<MesaEntity>> ObterTodasAsMesas()
    {
        return await _dbContext.Mesas!.Include(m => m.Consumidores).ToListAsync();
    }

    public async Task<MesaEntity?> ObterMesaPorNumero(int numeroMesa)
    {
        return await GetMesaByNumeroAsync(numeroMesa);
    }


    public async Task<bool> RemoveMesaPeloNumero(int mesaNumber)
    {
        var mesa = await GetMesaByNumeroAsync(mesaNumber);
        if (mesa == null)
        {
            return false;
        }

        _dbContext.Consumidores!.RemoveRange(mesa.Consumidores!);
        _dbContext.Mesas!.Remove(mesa);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveTodasMesasEReiniciaId()
    {
        var hasMesas = await _dbContext.Mesas!.AnyAsync();
        if (!hasMesas)
        {
            return false;
        }

        _dbContext.Consumidores!.RemoveRange(_dbContext.Consumidores);
        _dbContext.Mesas!.RemoveRange(_dbContext.Mesas);
        await _dbContext.SaveChangesAsync();

        ResetIdentityCounter(typeof(MesaEntity));
        ResetIdentityCounter(typeof(ConsumidorEntity));

        return true;
    }
    private void ResetIdentityCounter(Type entityType)
    {
        var tableName = _dbContext.Model.FindEntityType(entityType)!.GetTableName();
        var sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
        _dbContext.Database.ExecuteSqlRaw(sql);
    }

    public async Task<ConsumoMesaDTO?> ObterConsumoTotalMesa(int mesaNumero)
    {
        var mesa = await GetMesaByNumeroAsync(mesaNumero);
        if (mesa == null)
        {
            return null;
        }

        var consumoMesaDto = new ConsumoMesaDTO
        {
            MesaNumero = mesa.NumeroMesa!.Value,
            TotalMesa = mesa.TotalConsumido,
            Consumidores = GetConsumoIndividual(mesa)
        };

        return consumoMesaDto;
    }

    private List<ConsumoIndividualDTO> GetConsumoIndividual(MesaEntity mesa)
    {
        if (mesa.Consumidores == null)
        {
            return new List<ConsumoIndividualDTO>();
        }

        return mesa.Consumidores.Select(consumidor =>
        {
            var produtosConsumidos = consumidor.Pedidos == null
                ? new List<ProdutoDTO>()
                : consumidor.Pedidos.Select(p => new ProdutoDTO
                {
                    Id = p.Produto!.Id,
                    Nome = p.Produto.Nome,
                    Preco = p.Produto.Preco,
                    Categoria = p.Produto.Categoria
                }).ToList();

            return new ConsumoIndividualDTO
            {
                ConsumidorId = consumidor.Id!.Value,
                ConsumidorNome = consumidor.Nome,
                TotalIndividual = consumidor.TotalConsumido,
                ProdutosConsumidos = produtosConsumidos
            };
        }).ToList();
    }



    private async Task<MesaEntity?> GetMesaByNumeroAsync(int mesaNumero)
    {
        return await _dbContext.Mesas!
            .Include(m => m.Consumidores)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);
    }
}

