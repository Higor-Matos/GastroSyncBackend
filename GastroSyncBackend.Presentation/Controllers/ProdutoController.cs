using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace GastroSyncBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IAppDbContext _context;
    private readonly ILogger<ProdutoController> _logger;


    public ProdutoController(IAppDbContext context, ILogger<ProdutoController> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogInformation("ProdutoController foi instanciado");
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string tipo)
    {
        _logger.LogInformation("Entrou no método GetAll");
        try
        {
            if (string.IsNullOrEmpty(tipo))
            {
                _logger.LogInformation("GetAll: Retornando todos os produtos");
                return Ok(_context.Produtos!.ToList());
            }

            var produtos = tipo.ToLower() switch
            {
                "comida" => _context.Produtos!.Where(p => p.Categoria!.ToLower() == "comida").ToList(),
                "bebida" => _context.Produtos!.Where(p => p.Categoria!.ToLower() == "bebida").ToList(),
                _ => null
            };

            if (produtos == null)
            {
                _logger.LogWarning("GetAll: Tipo inválido. Escolha 'comida' ou 'bebida'.");
                return NotFound("Tipo inválido. Escolha 'comida' ou 'bebida'.");
            }

            _logger.LogInformation("GetAll: Produtos do tipo {Tipo} buscados com sucesso", tipo);
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError("GetAll: Erro ao executar o método - {MensagemErro}", ex.Message);  // Modelo padronizado
            return BadRequest("Ocorreu um erro ao buscar os produtos");
        }
    }
    
}