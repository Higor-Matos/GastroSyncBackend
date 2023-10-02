using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IProdutoService
{
    Task<ServiceResponse<List<ProdutoEntity>>> ObterTodosOsProdutos();
}