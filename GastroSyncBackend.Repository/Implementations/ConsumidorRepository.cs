using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace GastroSyncBackend.Repository.Implementations;

public class ConsumidorRepository : IConsumidorRepository
{
    private readonly IAppDbContext _dbContext;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public ConsumidorRepository(IAppDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores)
    {
        try
        {
            var mesa = await ObterMesaPorNumero(mesaId);
            if (mesa == null) return false;

            var consumidorEntities = consumidores.Select(nome => new ConsumidorEntity { Nome = nome, MesaId = mesaId }).ToList();
            mesa.Consumidores!.AddRange(consumidorEntities);
            await _dbContext.SaveChangesAsync();

            Logger.Info("Consumidores adicionados com sucesso à mesa.");

            return true;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Erro ao adicionar consumidores à mesa.");

            return false;
        }
    }

    private async Task<MesaEntity?> ObterMesaPorNumero(int mesaNumero) =>
        await _dbContext.Mesas!
            .Include(m => m.Consumidores)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.NumeroMesa == mesaNumero);

    public async Task<bool> AtualizarConsumidor(ConsumidorEntity consumidor)
    {
        try
        {
            _dbContext.Consumidores!.Update(consumidor);
            await _dbContext.SaveChangesAsync();

            Logger.Info("Consumidor atualizado com sucesso.");

            return true;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Erro ao atualizar o consumidor.");

            return false;
        }
    }

    public async Task<ConsumidorEntity?> ObterConsumidorPorId(int id)
    {
        try
        {
            var consumidor = await _dbContext.Consumidores!
                .Include(c => c.Pedidos)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            Logger.Info("Consumidor obtido com sucesso.");

            return consumidor;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Erro ao obter o consumidor por ID.");

            return null;
        }
    }
}