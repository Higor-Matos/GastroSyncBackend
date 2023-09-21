using GastroSyncBackend.Common;

namespace GastroSyncBackend.Services.Interfaces;

[AutoDI]
public interface IDatabaseInitializerService
{
    void SeedDatabase();
}