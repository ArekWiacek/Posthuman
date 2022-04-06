using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Posthuman.WebApi.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
