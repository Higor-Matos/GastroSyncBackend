using GastroSyncBackend.Domain.Response;
using GastroSyncBackend.Repository.Interfaces;
using GastroSyncBackend.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GastroSyncBackend.Services.Implementations
{
    public class ConsumidorService : IConsumidorService
    {
        private readonly IConsumidorRepository _consumidorRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ConsumidorService(IConsumidorRepository consumidorRepository) => _consumidorRepository = consumidorRepository;

        public async Task<ServiceResponse<bool>> AdicionarConsumidoresMesa(int mesaId, List<string> consumidores)
        {
            try
            {
                var result = await _consumidorRepository.AdicionarConsumidoresMesa(mesaId, consumidores);

                Logger.Info("Operação concluída com sucesso.");

                return new ServiceResponse<bool>(result, "Operação concluída", result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao executar a operação.");

                return new ServiceResponse<bool>(false, "Erro ao executar a operação", false);
            }
        }
    }
}