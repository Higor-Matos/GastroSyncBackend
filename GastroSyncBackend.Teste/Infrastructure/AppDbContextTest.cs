using GastroSyncBackend.Domain.Entities;
using GastroSyncBackend.Infrastructure.Implementations.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GastroSyncBackend.Teste.Infrastructure;

public class AppDbContextTests
{
    private readonly Mock<ILogger<AppDbContext>> _loggerMock = new();

    [Fact]
    public void AppDbContext_Creation_Succeeds()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        var dbContext = new AppDbContext(options, _loggerMock.Object);
        Assert.NotNull(dbContext);
    }

    [Fact]
    public void OnModelCreating_Configurations_AreValid()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbOnModel")
            .Options;
        using var dbContext = new AppDbContext(options, _loggerMock.Object);

        var model = dbContext.Model;
        var entity = model.FindEntityType(typeof(ProdutoEntity));

        Assert.NotNull(entity?.FindPrimaryKey());
        Assert.NotNull(entity.FindProperty("Id"));
        Assert.NotNull(entity.FindProperty("Preco"));
    }

    [Fact]
    public void EnsureDatabaseCreated_DatabaseIsCreated()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDbEnsureCreated")
            .Options;
        using var dbContext = new AppDbContext(options, _loggerMock.Object);
        dbContext.EnsureDatabaseCreated();

        Assert.True(dbContext.Database.IsInMemory());
    }
}