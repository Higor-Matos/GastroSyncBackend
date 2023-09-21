using Autofac;
using Autofac.Extensions.DependencyInjection;
using GastroSyncBackend.Common;
using GastroSyncBackend.Infrastructure.Implementations.DbContexts;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
ConfigureApp(builder);

var app = builder.Build();

try
{
    logger.Info("Configurando e construindo a aplicação...");

    InitializeDatabase(app);

    if (app.Environment.IsDevelopment())
    {
        EnableSwagger(app);
    }

    ConfigureAppMiddleware(app);

    logger.Info("Aplicação iniciada e pronta para receber requisições...");

    app.Run();

    logger.Info("Aplicação finalizada com sucesso.");
}
catch (Exception ex)
{
    logger.Fatal(ex, "Exceção crítica encontrada. Aplicação encerrada.");
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

    // Configuração do DbContext
    builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    var containerBuilder = new ContainerBuilder();
    ConfigureContainer(containerBuilder, builder.Services);
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
}

void ConfigureContainer(ContainerBuilder containerBuilder, IServiceCollection services)
{
    // Varredura de Assembly e Atributo AutoDI
    containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
        .Where(t => t.GetCustomAttributes(typeof(AutoDIAttribute), false).Any())
        .AsImplementedInterfaces();

    containerBuilder.Populate(services);
}

void InitializeDatabase(IHost app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
    dbContext.EnsureDatabaseCreated();
    logger.Info("Banco de dados verificado com sucesso.");
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
