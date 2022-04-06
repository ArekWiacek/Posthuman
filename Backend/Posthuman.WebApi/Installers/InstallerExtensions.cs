using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;

namespace Posthuman.WebApi.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var exportedTypes = typeof(Startup).Assembly.ExportedTypes;
            var installers = exportedTypes.Where(et => 
                typeof(IInstaller).IsAssignableFrom(et) && 
                !et.IsInterface && 
                !et.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(i => i.InstallServices(services, configuration));
        }
    }
}
