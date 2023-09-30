using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services.Implementations;

public class ConsumidorService : IConsumidorService
{
    private readonly IConsumidorRepository _consumidorRepository;

    public ConsumidorService(IConsumidorRepository consumidorRepository) => _consumidorRepository = consumidorRepository;

    public async Task<ServiceResponse<bool>> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores)
    {
        var result = await _consumidorRepository.AdicionarConsumidoresMesa(mesaId, consumidores);
        return new ServiceResponse<bool>(result, "Operação concluída", result);
    }

    public async Task<ServiceResponse<List<ConsumidorEntity>>> ObterConsumidoresMesa(int mesaNumero)
    {
        var consumidores = await _consumidorRepository.ObterConsumidoresMesa(mesaNumero);
        return new ServiceResponse<List<ConsumidorEntity>>(consumidores != null, "Operação concluída", consumidores);
    }

    public async Task<ServiceResponse<ConsumoIndividualDTO>> ObterConsumoIndividualMesa(int mesaNumero, int consumidorId)
    {
        var consumo = await _consumidorRepository.ObterConsumoIndividualMesa(mesaNumero, consumidorId);
        return new ServiceResponse<ConsumoIndividualDTO>(consumo != null, "Operação concluída", consumo);
    }
}