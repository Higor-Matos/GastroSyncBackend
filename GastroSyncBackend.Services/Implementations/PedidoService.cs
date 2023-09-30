using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services.Implementations;

public class PedidoService : IPedidoService
{

    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository) => _pedidoRepository = pedidoRepository;

    public async Task<ServiceResponse<bool>> AdicionarPedidoConsumidorMesa(int mesaId, int consumidorId, int produtoId, int quantidade)
    {
        var result = await _pedidoRepository.AdicionarPedidoConsumidorMesa(mesaId, consumidorId, produtoId, quantidade);
        return new ServiceResponse<bool>(result, "Operação concluída", result);
    }
}