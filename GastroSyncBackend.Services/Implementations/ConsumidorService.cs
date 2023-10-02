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
}