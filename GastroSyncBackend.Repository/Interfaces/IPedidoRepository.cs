using GastroSyncBackend.Common;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IPedidoRepository
{
    Task<bool> AdicionarPedidoConsumidorMesa(int mesaId, int consumidorId, int produtoId, int quantidade);
    Task<bool> AdicionarPedidoDividido(int mesaId, int[] consumidoresIds, int produtoId, int quantidade);
}