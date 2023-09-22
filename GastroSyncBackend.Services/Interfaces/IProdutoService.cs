using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IProdutoService
{
    List<ProdutoEntity> GetProdutos();
    public List<ProdutoEntity> GetProdutosByCategoria(string categoria);
}