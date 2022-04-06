using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posthuman.Core.Models.DTO;
using PosthumanWebApi.Controllers;
using System.Reflection;

namespace Posthuman.WebApi.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // TODO: WTF is this
            var assembly1 = typeof(TodoItemDTO).Assembly;
            var assembly2 = typeof(ProjectsController).Assembly;
            services.AddAutoMapper(new Assembly[] { assembly1, assembly2 });
        }
    }
}
