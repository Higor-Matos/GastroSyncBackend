using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IProdutoService
{
    List<Produto> GetProdutos();
}