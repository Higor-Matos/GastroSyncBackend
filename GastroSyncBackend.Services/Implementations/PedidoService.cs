using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Services.Implementations;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly ILogger<PedidoService> _logger;

    public PedidoService(IPedidoRepository pedidoRepository, ILogger<PedidoService> logger)
    {
        _pedidoRepository = pedidoRepository;
        _logger = logger;
    }

    public async Task<ServiceResponse<bool>> AdicionarPedidoIndividual(int mesaId, int consumidorId, int produtoId, int quantidade)
    {
        try
        {
            var result = await _pedidoRepository.AdicionarPedidoIndividual(mesaId, consumidorId, produtoId, quantidade);
            _logger.LogInformation("Pedido individual adicionado com sucesso.");
            return new ServiceResponse<bool>(result, "Operação concluída", result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar pedido individual.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao adicionar o pedido individual.");
        }
    }

    public async Task<ServiceResponse<bool>> AdicionarPedidoDividido(int mesaId, int[] consumidoresIds, int produtoId, int quantidade)
    {
        try
        {
            var result = await _pedidoRepository.AdicionarPedidoDividido(mesaId, consumidoresIds, produtoId, quantidade);
            _logger.LogInformation("Pedido dividido adicionado com sucesso.");
            return new ServiceResponse<bool>(result, "Operação concluída", result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar pedido dividido.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao adicionar o pedido dividido.");
        }
    }
}