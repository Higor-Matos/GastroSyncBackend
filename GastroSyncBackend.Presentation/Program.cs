using Autofac;
using Autofac.Extensions.DependencyInjection;
using GastroSyncBackend.Common;
using GastroSyncBackend.Infrastructure.Implementations.DbContexts;
using GastroSyncBackend.Infrastructure.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext
const string connectionString = "Server=sqlserver,1433;Database=GastroSyncDb;User Id=sa;Password=MyPass@word;Encrypt=False;";
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var containerBuilder = new ContainerBuilder();

// Varredura de Assembly e Atributo AutoDI
containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
    .Where(t => t.GetCustomAttributes(typeof(AutoDIAttribute), false).Any())
    .AsImplementedInterfaces();

containerBuilder.Populate(builder.Services);
var container = containerBuilder.Build();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddAutofac();

var app = builder.Build();

try
{
    logger.Info("Configurando e construindo a aplicação...");

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        // Tente criar o banco de dados, se ele não existir
        dbContext.EnsureDatabaseCreated();
        logger.Info("Banco de dados verificado com sucesso.");
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.Info("Swagger habilitado no ambiente de desenvolvimento.");
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

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
