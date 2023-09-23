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
    }

    public void SeedDatabase()
    {
        try
        {
            if (_context.Produtos == null || _context.Produtos.Any()) return;
            _logger.LogInformation("DatabaseInitializerService: Semeadura do banco de dados concluída com sucesso");

        }
        catch (Exception ex)
        {
            _logger.LogError("DatabaseInitializerService: Erro durante a semeadura do banco de dados - {MensagemErro}", ex.Message);
        }
    }
}