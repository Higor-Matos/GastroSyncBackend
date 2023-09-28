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
        return await _dbContext.Mesas!
            .Where(m => m.NumeroMesa == numeroMesa)
            .Include(m => m.Consumidores)
            .FirstOrDefaultAsync();
    }


    public async Task<bool> RemoveMesaPeloNumero(int mesaNumber)
    {
        var mesa = await _dbContext.Mesas!.Include(m => m.Consumidores)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumber);
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
        var mesas = await _dbContext.Mesas!.ToListAsync();
        if (!mesas.Any())
        {
            return false;
        }

        _dbContext.Consumidores!.RemoveRange(_dbContext.Consumidores);
        _dbContext.Mesas!.RemoveRange(mesas);
        await _dbContext.SaveChangesAsync();

        // Redefine o contador de identidade para Mesas
        var mesaEntityType = _dbContext.Model.FindEntityType(typeof(MesaEntity));
        var mesaTableName = mesaEntityType!.GetTableName();
        var mesaSql = $"DBCC CHECKIDENT ('{mesaTableName}', RESEED, 0)";
        _dbContext.Database.ExecuteSqlRaw(mesaSql);

        // Redefine o contador de identidade para Consumidores
        var consumidorEntityType = _dbContext.Model.FindEntityType(typeof(ConsumidorEntity));
        var consumidorTableName = consumidorEntityType!.GetTableName();
        var consumidorSql = $"DBCC CHECKIDENT ('{consumidorTableName}', RESEED, 0)";
        _dbContext.Database.ExecuteSqlRaw(consumidorSql);

        return true;
    }

    public async Task<ConsumoMesaDTO?> ObterConsumoTotalMesa(int mesaNumero)
    {
        var mesa = await _dbContext.Mesas!
            .Include(m => m.Consumidores)!
            .ThenInclude(c => c.Pedidos)!
            .ThenInclude(p => p.Produto)
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);

        if (mesa == null)
        {
            return null;
        }

        var consumoMesaDto = new ConsumoMesaDTO
        {
            MesaNumero = mesa.NumeroMesa!.Value,
            TotalMesa = mesa.TotalConsumido,
            Consumidores = new List<ConsumoIndividualDTO>()
        };

        foreach (var consumoIndividualDto in mesa.Consumidores!.Select(consumidor => new ConsumoIndividualDTO
        {
            ConsumidorId = consumidor.Id!.Value,
            ConsumidorNome = consumidor.Nome,
            TotalIndividual = consumidor.TotalConsumido,
            ProdutosConsumidos = consumidor.Pedidos!.Select(p => new ProdutoDTO
            {
                Id = p.Produto!.Id,
                Nome = p.Produto.Nome,
                Preco = p.Produto.Preco,
                Categoria = p.Produto.Categoria
            }).ToList()
        }))
        {
            consumoMesaDto.Consumidores.Add(consumoIndividualDto);
        }

        return consumoMesaDto;
    }
}

