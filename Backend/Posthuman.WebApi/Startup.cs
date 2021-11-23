using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Repositories;
using Posthuman.Core.Services;
using Posthuman.Data;
using Posthuman.Data.Repositories;
using Posthuman.Services;
using PosthumanWebApi.Controllers;
using Posthuman.RealTime.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using Posthuman.WebApi.Middleware;

namespace Posthuman.WebApi
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();

            BuildServices(services);
            BuildAutoMapper(services);
            BuildSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Configuring Posthuman app...");

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "PosthumanWebApi v1"));

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseCors("ClientPermission");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationsHub>("notifications");
            });

            logger.LogInformation("Posthuman configuration done!");
        }

        private void BuildServices(IServiceCollection services)
        {
            var envType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (envType == null)
            {
                envType = "Production";
            }

            services
                .AddDbContext<PosthumanContext>(options => options
                    .UseSqlServer(GetConnectionString(envType),
                        x => x.MigrationsAssembly("Posthuman.Data")));

            services.AddCors(options =>
            {
                var originHost = GetFrontendUrl(envType);

                options.AddPolicy("ClientPermission", policy =>
                {
                    policy
                    .WithOrigins(
                        "http://posthumanae-001-site1.itempurl.com",
                        "http://localhost:3000",
                        "http://localhost:7201",
                        "http://posthumanbackapp-001-site1.btempurl.com",
                        "posthumanbackapp-001-site1.btempurl.com",
                        "posthumanae-001-site1.itempurl.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.WithOrigins(originHost)
                    .AllowCredentials();
                });
            });

            services.AddControllers();
            services.AddSignalR();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationsService, NotificationsService>();

            services.AddTransient<ITodoItemsRepository, TodoItemsRepository>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();
            services.AddTransient<IEventItemsRepository, EventItemsRepository>();
            services.AddTransient<IAvatarsRepository, AvatarsRepository>();
            services.AddTransient<IBlogPostsRepository, BlogPostsRepository>();
            services.AddTransient<IRewardCardsRepository, RewardCardsRepository>();

            services.AddTransient<ITodoItemsService, TodoItemsService>();
            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IEventItemsService, EventItemsService>();
            services.AddTransient<IAvatarsService, AvatarsService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();
            services.AddTransient<IRewardCardsService, RewardCardsService>();
        }

        private void BuildAutoMapper(IServiceCollection services)
        {
            // TODO: WTF is this
            var assembly1 = typeof(TodoItemDTO).Assembly;
            var assembly2 = typeof(ProjectsController).Assembly;
            services.AddAutoMapper(new Assembly[] { assembly1, assembly2 });
        }

        private void BuildSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "PosthumanWebApi",
                    Version = "v1"
                });
            });
        }

        private string GetConnectionString(string envType)
        {
            string? dbConnectionString;

            if (envType == "Development")
                dbConnectionString = Configuration.GetConnectionString("PosthumanDatabaseDevelopment");
            else
                dbConnectionString = Configuration.GetConnectionString("PosthumanDatabaseProduction");

            return dbConnectionString;
        }

        private string GetFrontendUrl(string envType)
        {
            string? hostUrl;

            if (envType == "Development")
                hostUrl = Configuration.GetSection("FrontendUrl").GetValue<string>("Development");
            else
                hostUrl = Configuration.GetSection("FrontendUrl").GetValue<string>("Production");

            return hostUrl;
        }

        private string GetBackendUrl(string envType)
        {
            string? hostUrl;

            if (envType == "Development")
                hostUrl = Configuration.GetSection("BackendUrl").GetValue<string>("Development");
            else
                hostUrl = Configuration.GetSection("BackendUrl").GetValue<string>("Production");

            return hostUrl;
        }

    }
}
