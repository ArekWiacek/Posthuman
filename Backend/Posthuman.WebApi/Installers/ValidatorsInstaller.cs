using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Models.Validators;

namespace Posthuman.WebApi.Installers
{
    public class ValidatorsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
        }
    }
}
