using GastroSyncBackend.Common;
using GastroSyncBackend.Domain.Entities;

namespace GastroSyncBackend.Repository.Interfaces;

[AutoDI]
public interface IMesaRepository
{
    Mesa GetMesaById(int id);
    void AddProdutoToMesa(int mesaId, Produto produto);
}