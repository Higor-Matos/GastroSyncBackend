using GastroSyncBackend.Domain.DTOs;
using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;

namespace GastroSyncBackend.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }


    public async Task<ServiceResponse<MesaEntity>> CriarMesa(int numeroMesa, string local)
    {
        var mesaExistente = await ObterMesaPorNumero(numeroMesa);
        if (mesaExistente.Success)
        {
            return new ServiceResponse<MesaEntity>(false, "Operação concluída");
        }

        var mesa = await _mesaRepository.CriarMesa(numeroMesa, local);
        return new ServiceResponse<MesaEntity>(true, "Operação concluída", mesa);
    }

    public async Task<ServiceResponse<bool>> RemoveMesaPeloNumero(int mesaNumber)
    {
        var isRemoved = await _mesaRepository.RemoveMesaPeloNumero(mesaNumber);
        return new ServiceResponse<bool>(isRemoved, "Operação concluída", isRemoved);
    }


    public async Task<ServiceResponse<bool>> RemoveTodasMesasEReiniciaId()
    {
        var isRemoved = await _mesaRepository.RemoveTodasMesasEReiniciaId();
        return new ServiceResponse<bool>(isRemoved, "Operação concluída", isRemoved);
    }


    public async Task<ServiceResponse<IEnumerable<MesaEntity>>> ObterTodasAsMesas()
    {
        var mesas = await _mesaRepository.ObterTodasAsMesas();
        var mesaEntities = mesas as MesaEntity[] ?? mesas.ToArray();
        return new ServiceResponse<IEnumerable<MesaEntity>>(mesaEntities.Any(), "Operação concluída", mesaEntities);
    }

    public async Task<ServiceResponse<MesaEntity>> ObterMesaPorNumero(int numeroMesa)
    {
        var mesa = await _mesaRepository.ObterMesaPorNumero(numeroMesa);
        return new ServiceResponse<MesaEntity>(mesa != null, "Operação concluída", mesa);
    }


    public async Task<ServiceResponse<bool>> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores)
    {
        var result = await _mesaRepository.AdicionarConsumidoresMesa(mesaId, consumidores);
        return new ServiceResponse<bool>(result, "Operação concluída", result);
    }

    public async Task<ServiceResponse<List<ConsumidorEntity>>> ObterConsumidoresMesa(int mesaNumero)
    {
        var consumidores = await _mesaRepository.ObterConsumidoresMesa(mesaNumero);
        return new ServiceResponse<List<ConsumidorEntity>>(consumidores != null, "Operação concluída", consumidores);
    }

    public async Task<ServiceResponse<bool>> AdicionarPedidoConsumidorMesa(int mesaId, int consumidorId, int produtoId, int quantidade)
    {
        var result = await _mesaRepository.AdicionarPedidoConsumidorMesa(mesaId, consumidorId, produtoId, quantidade);
        return new ServiceResponse<bool>(result, "Operação concluída", result);
    }

    public async Task<ServiceResponse<ConsumoMesaDTO>> ObterConsumoTotalMesa(int mesaNumero)
    {
        var consumo = await _mesaRepository.ObterConsumoTotalMesa(mesaNumero);
        return new ServiceResponse<ConsumoMesaDTO>(consumo != null, "Operação concluída", consumo);
    }

    public async Task<ServiceResponse<ConsumoIndividualDTO>> ObterConsumoTotalMesa(int mesaNumero, int consumidorId)
    {
        var consumo = await _mesaRepository.ObterConsumoTotalMesa(mesaNumero, consumidorId);
        return new ServiceResponse<ConsumoIndividualDTO>(consumo != null, "Operação concluída", consumo);
    }
}