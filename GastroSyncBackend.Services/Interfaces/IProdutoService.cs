using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IProdutoService
{
    Task<List<ProdutoEntity>> GetProdutosAsync();
    Task<List<ProdutoEntity>> GetProdutosByCategoriaAsync(string categoria);
}