using GastroSyncBackend.Infrastructure.Implementations.DbContexts;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using GastroSyncBackend.Presentation.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
ConfigureApp(builder);

var app = builder.Build();

try
{
    logger.Info("Configurando e construindo a aplica��o...");

    var initializeAndMigrateDatabase = InitializeAndMigrateDatabase(app);

    logger.Info("Banco de dados verificado com sucesso.");


    EnableSwagger(app);
    

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
            sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlOptions.EnableRetryOnFailure();
            }));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyAllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GastroSync V.6");
        c.RoutePrefix = string.Empty;
    });
    logger.Info("Swagger habilitado no ambiente de desenvolvimento.");
}

void ConfigureAppMiddleware(WebApplication app)
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseCors("MyAllowSpecificOrigins");

    app.UseAuthorization();
    app.MapControllers();
}
