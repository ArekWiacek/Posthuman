using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Posthuman.WebApi.Installers
{
    public class WebServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                // var originHost = GetFrontendUrl(environmentType);
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy
                    .WithOrigins(
                        "http://posthuman.pl",
                        "http://posthumanae-001-site1.itempurl.com",
                        "http://localhost:3000",
                        "http://localhost:7201",
                        "http://posthumanbackapp-001-site1.btempurl.com",
                        "posthumanbackapp-001-site1.btempurl.com",
                        "posthumanae-001-site1.itempurl.com",
                        "https://red-robot-490980.postman.co/")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddControllers().AddFluentValidation();
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "PosthumanWebApi",
                    Version = "v1"
                });
            });
        }
    }
}
