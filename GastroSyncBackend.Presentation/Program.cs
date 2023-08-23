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
    logger.Info("Aplica��o Iniciando...");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        logger.Info("Swagger habilitado no ambiente de desenvolvimento.");
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    logger.Info("Aplica��o Configurada. Iniciando...");

    app.Run();

    logger.Info("Aplica��o Iniciada.");
}
catch (Exception ex)
{
    logger.Error(ex, "Aplica��o encerrada devido a uma exce��o.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}