using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Repository.Interfaces;

namespace GastroSyncBackend.Services;

public class MesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }

    public Mesa GetMesaById(int id)
    {
        return _mesaRepository.GetMesaById(id);
    }

    public void AddProdutoToMesa(int mesaId, Produto produto)
    {
        _mesaRepository.AddProdutoToMesa(mesaId, produto);
    }
}