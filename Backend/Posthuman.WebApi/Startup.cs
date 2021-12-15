using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Posthuman.Core;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Repositories;
using Posthuman.Core.Services;
using Posthuman.Data;
using Posthuman.Data.Repositories;
using Posthuman.Services;
using PosthumanWebApi.Controllers;
using Posthuman.RealTime.Notifications;
using Posthuman.WebApi.Middleware;

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
            var environmentType = GetEnvironmentType();

            services
                .AddDbContext<PosthumanContext>(options => options
                    .UseSqlServer(GetConnectionString(environmentType),
                        x => x.MigrationsAssembly("Posthuman.Data")));

            services.AddCors(options =>
            {
                var originHost = GetFrontendUrl(environmentType);

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
                        "posthumanae-001-site1.itempurl.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.WithOrigins(originHost)
                    .AllowCredentials();
                });
            });

            services.AddControllers();

            //services.AddAuthentication()
            //    .AddFacebook(facebookOptions =>
            //    {
            //        facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //        facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //    });

            services.AddSignalR();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            AddRepositories(services);
            AddServices(services);
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITodoItemsRepository, TodoItemsRepository>();
            services.AddTransient<ITodoItemsCyclesRepository, TodoItemsCyclesRepository>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();
            services.AddTransient<IEventItemsRepository, EventItemsRepository>();
            services.AddTransient<IAvatarsRepository, AvatarsRepository>();
            services.AddTransient<IBlogPostsRepository, BlogPostsRepository>();
            services.AddTransient<ITechnologyCardsRepository, TechnologyCardsRepository>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ITodoItemsService, TodoItemsService>();
            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IEventItemsService, EventItemsService>();
            services.AddTransient<IAvatarsService, AvatarsService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();
            services.AddTransient<ITechnologyCardsService, TechnologyCardsService>();
            services.AddScoped<INotificationsService, NotificationsService>();
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

        private EnvironmentType GetEnvironmentType()
        {
            var environmentType = EnvironmentType.Development;
            var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentVariable == null)
                environmentType = EnvironmentType.Production;

            return environmentType;
        }

        private string GetConnectionString(EnvironmentType environmentType)
        {
            string? dbConnectionString;

            if (environmentType == EnvironmentType.Development)
                dbConnectionString = Configuration.GetConnectionString("PosthumanDatabaseDevelopment");
            else
                dbConnectionString = Configuration.GetConnectionString("PosthumanDatabaseProduction");

            return dbConnectionString;
        }

        private string GetFrontendUrl(EnvironmentType environmentType)
        {
            string? hostUrl;

            if (environmentType == EnvironmentType.Development)
                hostUrl = Configuration.GetSection("FrontendUrl").GetValue<string>("Development");
            else
                hostUrl = Configuration.GetSection("FrontendUrl").GetValue<string>("Production");

            return hostUrl;
        }

        private string GetBackendUrl(EnvironmentType environmentType)
        {
            string? hostUrl;

            if (environmentType == EnvironmentType.Development)
                hostUrl = Configuration.GetSection("BackendUrl").GetValue<string>("Development");
            else
                hostUrl = Configuration.GetSection("BackendUrl").GetValue<string>("Production");

            return hostUrl;
        }

    }
}
