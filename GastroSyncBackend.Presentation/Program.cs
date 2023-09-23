using GastroSyncBackend.Infrastructure.Implementations.DbContexts;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Presentation.Extensions;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
ConfigureApp(builder);

var app = builder.Build();

try
{
    logger.Info("Configurando e construindo a aplica��o...");

    InitializeAndMigrateDatabase(app);

    logger.Info("Banco de dados verificado com sucesso.");

    if (app.Environment.IsDevelopment())
    {
        EnableSwagger(app);
    }

    ConfigureAppMiddleware(app);

    logger.Info("Aplica��o iniciada e pronta para receber requisi��es...");

    app.Run();

    logger.Info("Aplica��o finalizada com sucesso.");
}
catch (Exception ex)
{
    logger.Fatal(ex, "Exce��o cr�tica encontrada. Aplica��o encerrada.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

void ConfigureApp(WebApplicationBuilder builder)
{
    builder.Host.UseNLog();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var configuration = builder.Configuration;

    // Configura��o do DbContext
    builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

    // Classe de resolu��o de depend�ncias
    var result = builder.Services.AddServicesWithAutoDi();

    if (result)
    {
        logger.Info("Todos os assemblies foram carregados com sucesso.");
    }
    else
    {
        logger.Error("Ocorreu um problema durante o carregamento dos assemblies.");
    }
}


async Task InitializeAndMigrateDatabase(IHost app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
    await dbContext.EnsureDatabaseCreatedAsync();
    await dbContext.DatabaseMigrateAsync();
    logger.Info("Banco de dados verificado e migrado com sucesso.");
}

void EnableSwagger(IApplicationBuilder app)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    logger.Info("Swagger habilitado no ambiente de desenvolvimento.");
}

void ConfigureAppMiddleware(WebApplication app)
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}
