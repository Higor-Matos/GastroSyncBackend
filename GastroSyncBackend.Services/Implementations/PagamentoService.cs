using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IConsumidorRepository _consumidorRepository; 

        public PagamentoService(
            IPagamentoRepository pagamentoRepository,
            IConsumidorRepository consumidorRepository)
        {
            _pagamentoRepository = pagamentoRepository;
            _consumidorRepository = consumidorRepository;
        }

        public async Task<ServiceResponse<bool>> RealizarPagamento(int consumidorId, decimal valor)
        {
            var consumidor = await _consumidorRepository.ObterConsumidorPorId(consumidorId);

            if (consumidor == null || consumidor.TotalConsumido < valor)
            {
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

            return new ServiceResponse<bool>(true, "Pagamento realizado com sucesso.");
        }
    }
}