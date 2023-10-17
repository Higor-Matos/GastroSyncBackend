using GastroSyncBackend.Domain.DTOs;
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
            if (!ValidarPagamento(consumidor, valor))
            {
                return new ServiceResponse<bool>(false, "Consumidor não encontrado ou valor insuficiente.");
            }

            var pagamento = CriarPagamentoEntity(consumidorId, valor);
            await _pagamentoRepository.CriarPagamento(pagamento);

            consumidor!.TotalConsumido -= valor;
            await _consumidorRepository.AtualizarConsumidor(consumidor);

            return new ServiceResponse<bool>(true, "Pagamento realizado com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar o pagamento.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao realizar o pagamento.");
        }
    }

    private static bool ValidarPagamento(ConsumidorEntity? consumidor, decimal valor)
    {
        return consumidor != null && consumidor.TotalConsumido >= valor;
    }

    private static PagamentoEntity CriarPagamentoEntity(int consumidorId, decimal valor)
    {
        return new PagamentoEntity
        {
            ConsumidorId = consumidorId,
            ValorPago = valor,
            DataPagamento = DateTime.Now
        };
    }

    public async Task<List<PagamentoDetalhadoDto>> ObterPagamentosDetalhados()
    {
        return await _pagamentoRepository.ObterPagamentosDetalhados();
    }

}