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
using Posthuman.RealTimeCommunication.Notifications;

namespace Posthuman.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BuildServices(services);
            BuildAutoMapper(services);
            BuildSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Todo - remove from here, now it stays for debugging purposes.
            // On production should be inside if(env.IsDevelopment())
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "PosthumanWebApi v1"));

            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //});

            app.UseCors("ClientPermission");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationsHub>("notifications");
            });
        }

        private string BuildConnectionString()
        {
            var envType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string? dbConnectionString;

            if (envType == "Development")
                dbConnectionString = Configuration.GetConnectionString("PosthumanDatabaseDevelopment");
            else
                dbConnectionString = Configuration.GetConnectionString("PosthumanDatabaseProduction");

            return dbConnectionString;
        }

        private void BuildServices(IServiceCollection services)
        {
            services
                .AddDbContext<PosthumanContext>(options => options
                    .UseSqlServer(BuildConnectionString(),
                        x => x.MigrationsAssembly("Posthuman.Data")));

            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
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

            services.AddTransient<ITodoItemsService, TodoItemsService>();
            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IEventItemsService, EventItemsService>();
            services.AddTransient<IAvatarsService, AvatarsService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();
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
    }
}
