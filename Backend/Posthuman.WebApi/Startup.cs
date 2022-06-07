using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Posthuman.RealTime.Notifications;
using Posthuman.WebApi.Middleware;
using Posthuman.WebApi.Installers;
using Posthuman.Services.BackgroundServices;

namespace Posthuman.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);

            // Temp
            services.AddHostedService<DailyRecalculationBackgroundService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Configuring Posthuman app...");

            SetExceptionHandlingMode(app, env, logger);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "PosthumanWebApi v1"));

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseCors("ClientPermission");
            app.UseMiddleware<JwtMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationsHub>("notifications");
            });

            logger.LogInformation("Posthuman configuration done!");
        }

        private void SetExceptionHandlingMode(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("Dev environment - developer exception page will be used.");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogInformation("Prod environment - /error exception handler page will be used.");
                app.UseExceptionHandler("/error");
            }
        }
    }
}
