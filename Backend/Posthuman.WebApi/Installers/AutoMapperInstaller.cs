using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posthuman.Core.Models.DTO;
using PosthumanWebApi.Controllers;
using System;
using System.Linq;
using System.Reflection;

namespace Posthuman.WebApi.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        /// <summary>
        /// Map only "Core" project with entities and WebApi project 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
                var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                var core = domainAssemblies.First(ass => ass.FullName.Contains("Posthuman.Core"));
                var webApi = domainAssemblies.First(ass => ass.FullName.Contains("Posthuman.WebApi"));

                if (core == null || webApi == null)
                    throw new Exception("Error loading assemblies for mapping registration.");

                services.AddAutoMapper(new Assembly[] { core, webApi });
            
        }
    }
}
