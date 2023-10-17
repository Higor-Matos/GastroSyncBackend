using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IPagamentoService
{
    Task<ServiceResponse<bool>> RealizarPagamento(int consumidorId, decimal valor);
    Task<List<PagamentoDetalhadoDto>> ObterPagamentosDetalhados();
}