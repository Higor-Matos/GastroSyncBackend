using GastroSyncBackend.Presentation.Extensions;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GastroSyncBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumidorController : ControllerBase
    {
        private readonly IConsumidorService _consumidorService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ConsumidorController(IConsumidorService consumidorService)
        {
            _consumidorService = consumidorService;
        }

        [HttpPost("{mesaId:int}/AdicionarConsumidoresMesa")]
        public async Task<IActionResult> AdicionarConsumidoresMesa(int mesaId, [FromBody] List<string> consumidores)
        {
            try
            {
                var result = await _consumidorService.AdicionarConsumidoresMesa(mesaId, consumidores);

                Logger.Info("Operação concluída com sucesso.");

                return this.ApiResponse(result.Success, result.Message, result.Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Erro ao executar a operação.");

                return this.ApiResponse(false, "Erro ao executar a operação", false);
            }
        }
    }
}