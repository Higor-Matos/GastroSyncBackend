using Autofac;
using Autofac.Extensions.DependencyInjection;
using GastroSyncBackend.Common;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var containerBuilder = new ContainerBuilder();
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
    logger.Info("Configurando e construindo a aplica��o...");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.Info("Swagger habilitado no ambiente de desenvolvimento.");
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

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