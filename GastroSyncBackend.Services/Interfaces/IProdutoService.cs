using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IProdutoService
{
    Task<ServiceResponse<List<ProdutoEntity>>> GetProdutosByCategoriaAsync(string categoria);
    Task<ServiceResponse<List<ProdutoEntity>>> GetProdutosAsync();
}