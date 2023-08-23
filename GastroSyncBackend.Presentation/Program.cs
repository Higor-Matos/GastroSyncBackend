using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = NLog.LogManager.GetCurrentClassLogger();

try
{
    logger.Info("Aplicação Iniciando...");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.Info("Swagger habilitado no ambiente de desenvolvimento.");
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    logger.Info("Aplicação Configurada. Iniciando...");

    app.Run();

    logger.Info("Aplicação Iniciada.");
}
catch (Exception ex)
{
    logger.Error(ex, "Aplicação encerrada devido a uma exceção.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}