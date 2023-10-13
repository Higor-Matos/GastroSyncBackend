using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;
    private readonly ILogger<MesaService> _logger;

    public MesaService(IMesaRepository mesaRepository, ILogger<MesaService> logger)
    {
        _mesaRepository = mesaRepository;
        _logger = logger;
    }

    public async Task<ServiceResponse<MesaEntity>> CriarMesa(int numeroMesa, string local)
    {
        try
        {
            var mesaExistente = await ObterMesaPorNumero(numeroMesa);
            if (mesaExistente.Success) return new ServiceResponse<MesaEntity>(false, "Operação concluída");
            var mesa = await _mesaRepository.CriarMesa(numeroMesa, local);
            _logger.LogInformation("Mesa criada com sucesso.");
            return new ServiceResponse<MesaEntity>(true, "Operação concluída", mesa);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar a mesa.");
            return new ServiceResponse<MesaEntity>(false, "Ocorreu um erro ao criar a mesa.");
        }
    }

    public async Task<ServiceResponse<bool>> RemoveMesaPeloNumero(int mesaNumber)
    {
        try
        {
            var isRemoved = await _mesaRepository.RemoveMesaPeloNumero(mesaNumber);
            _logger.LogInformation("Mesa removida com sucesso.");
            return new ServiceResponse<bool>(isRemoved, "Operação concluída", isRemoved);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover a mesa.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao remover a mesa.");
        }
    }

    public async Task<ServiceResponse<bool>> RemoveTodasMesasEReiniciaId()
    {
        try
        {
            var isRemoved = await _mesaRepository.RemoveTodasMesasEReiniciaId();
            _logger.LogInformation("Todas as mesas foram removidas com sucesso.");
            return new ServiceResponse<bool>(isRemoved, "Operação concluída", isRemoved);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover todas as mesas.");
            return new ServiceResponse<bool>(false, "Ocorreu um erro ao remover todas as mesas.");
        }
    }

    public async Task<ServiceResponse<IEnumerable<MesaEntity>>> ObterTodasAsMesas()
    {
        try
        {
            var mesas = await _mesaRepository.ObterTodasAsMesas();
            var mesaEntities = mesas as MesaEntity[] ?? mesas.ToArray();

            if (!mesaEntities.Any())
            {
                _logger.LogInformation("Nenhuma mesa foi encontrada.");
                return new ServiceResponse<IEnumerable<MesaEntity>>(true, "Nenhuma mesa foi encontrada.", mesaEntities);
            }

            _logger.LogInformation("Todas as mesas foram recuperadas com sucesso.");
            return new ServiceResponse<IEnumerable<MesaEntity>>(true, "Operação concluída", mesaEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter todas as mesas.");
            return new ServiceResponse<IEnumerable<MesaEntity>>(false, "Ocorreu um erro ao obter todas as mesas.");
        }
    }


    public async Task<ServiceResponse<MesaEntity>> ObterMesaPorNumero(int numeroMesa)
    {
        try
        {
            var mesa = await _mesaRepository.ObterMesaPorNumero(numeroMesa);
            _logger.LogInformation("Mesa obtida por número com sucesso.");
            return new ServiceResponse<MesaEntity>(mesa != null, "Operação concluída", mesa);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter a mesa por número.");
            return new ServiceResponse<MesaEntity>(false, "Ocorreu um erro ao obter a mesa por número.");
        }
    }
}