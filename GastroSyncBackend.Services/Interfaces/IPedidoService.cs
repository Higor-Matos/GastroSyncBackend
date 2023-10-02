using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IPedidoService
{
    Task<ServiceResponse<bool>> AdicionarPedidoConsumidorMesa(int mesaId, int consumidorId, int produtoId, int quantidade);

    Task<ServiceResponse<bool>> AdicionarPedidoDividido(int mesaId, int[] consumidoresIds, int produtoId,
        int quantidade);
}