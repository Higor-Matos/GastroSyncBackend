using GastroSyncBackend.Common;
using System.Reflection;

namespace GastroSyncBackend.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static bool AddServicesWithAutoDi(this IServiceCollection services)
    {
        try
        {
            // Carregar todos os assemblies
            foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            {
                Assembly.LoadFrom(file);
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var typesWithAutoDi = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(AutoDIAttribute), false).Any());

                foreach (var type in typesWithAutoDi)
                {
                    if (!type.IsInterface) continue;
                    var implementationType = assembly.GetTypes().FirstOrDefault(t => t.GetInterfaces().Contains(type));

                    if (implementationType == null) continue;
                    services.AddTransient(type, implementationType);
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}