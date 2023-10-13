using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Services.Implementations;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IConsumidorRepository _consumidorRepository;
    private readonly ILogger<PagamentoService> _logger;

    public PagamentoService(
        IPagamentoRepository pagamentoRepository,
        IConsumidorRepository consumidorRepository,
        ILogger<PagamentoService> logger)
    {
        _pagamentoRepository = pagamentoRepository;
        _consumidorRepository = consumidorRepository;
        _logger = logger;
    }

    public async Task<ServiceResponse<bool>> RealizarPagamento(int consumidorId, decimal valor)
    {
        try
        {
            var consumidor = await _consumidorRepository.ObterConsumidorPorId(consumidorId);

            if (consumidor == null || consumidor.TotalConsumido < valor)
            {
                _logger.LogWarning("Falha no pagamento: Consumidor não encontrado ou valor insuficiente.");
                return new ServiceResponse<bool>(false, "Consumidor não encontrado ou valor insuficiente.");
            }

            var pagamento = new PagamentoEntity
            {
                ConsumidorId = consumidorId,
                ValorPago = valor,
                DataPagamento = DateTime.Now
            };

            await _pagamentoRepository.CriarPagamento(pagamento);

            consumidor.TotalConsumido -= valor;
            await _consumidorRepository.AtualizarConsumidor(consumidor);

            _logger.LogInformation("Pagamento realizado com sucesso.");
            return new ServiceResponse<bool>(true, "Pagamento realizado com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar o pagamento.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao realizar o pagamento.");
        }
    }
}