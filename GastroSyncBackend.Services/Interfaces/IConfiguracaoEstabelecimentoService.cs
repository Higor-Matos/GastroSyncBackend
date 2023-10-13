using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IConfiguracaoEstabelecimentoService
{
    Task<ServiceResponse<bool>> AtivarCover();
    Task<ServiceResponse<bool>> DesativarCover();
    Task<ServiceResponse<CoverStatusDTO>> ObterStatusCover();
    Task<ServiceResponse<bool>> AtualizarValorCover(decimal novoValor);

}