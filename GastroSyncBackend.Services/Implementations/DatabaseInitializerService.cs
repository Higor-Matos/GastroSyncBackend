using GastroSyncBackend.Infrastructure.Implementations.DbContexts;
using GastroSyncBackend.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GastroSyncBackend.Services.Implementations;

public class DatabaseInitializerService : IDatabaseInitializerService
{
    private readonly AppDbContext _context;
    private readonly ILogger<DatabaseInitializerService> _logger;

    public DatabaseInitializerService(AppDbContext context,
        ILogger<DatabaseInitializerService> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogInformation("DatabaseInitializerService foi instanciado");
    }

    public void SeedDatabase()
    {
        _logger.LogInformation("DatabaseInitializerService: Iniciando a semeadura do banco de dados");

        try
        {
            if (_context.Produtos != null && !_context.Produtos.Any())
            {
                _logger.LogInformation("DatabaseInitializerService: Semeadura do banco de dados concluída com sucesso");
            }
            else
            {
                _logger.LogWarning("DatabaseInitializerService: O banco de dados já contém produtos. Semeadura não necessária");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("DatabaseInitializerService: Erro durante a semeadura do banco de dados - {MensagemErro}", ex.Message);
        }
    }
}