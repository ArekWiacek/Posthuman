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
using Microsoft.AspNetCore.Identity;
using Posthuman.Core.Models.Entities;
using FluentValidation;
using Posthuman.Core.Models.Validators;
using FluentValidation.AspNetCore;
using Posthuman.WebApi.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Posthuman.Shared;

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
            AddAuthentication(services);
            AddDotnetServices(services);
            AddCustomRepositories(services);
            AddCustomServices(services);
            AddValidators(services);
            AddAutoMapper(services);
            AddSwagger(services);
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

        private void AddAuthentication(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
            //.AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Role", "Admin"));
                options.AddPolicy("UsersOnly", policy => policy.RequireClaim("Role", "Admin", "User"));
            });
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

        private void AddDotnetServices(IServiceCollection services)
        {
            var environmentType = GetEnvironmentType();

            services
                .AddDbContext<PosthumanContext>(options => options
                    .UseSqlServer(GetConnectionString(environmentType),
                        x => x.MigrationsAssembly("Posthuman.Data")));

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
        }

        private void AddCustomRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITodoItemsRepository, TodoItemsRepository>();
            services.AddTransient<ITodoItemsCyclesRepository, TodoItemsCyclesRepository>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();
            services.AddTransient<IEventItemsRepository, EventItemsRepository>();
            services.AddTransient<IAvatarsRepository, AvatarsRepository>();
            services.AddTransient<IBlogPostsRepository, BlogPostsRepository>();
            services.AddTransient<ITechnologyCardsRepository, TechnologyCardsRepository>();
        }

        private void AddCustomServices(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITodoItemsService, TodoItemsService>();
            services.AddTransient<ITodoItemsCyclesService, TodoItemsCyclesService>();
            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IEventItemsService, EventItemsService>();
            services.AddTransient<IAvatarsService, AvatarsService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();
            services.AddTransient<ITechnologyCardsService, TechnologyCardsService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }

        private void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            // TODO: WTF is this
            var assembly1 = typeof(TodoItemDTO).Assembly;
            var assembly2 = typeof(ProjectsController).Assembly;
            services.AddAutoMapper(new Assembly[] { assembly1, assembly2 });
        }

        private void AddSwagger(IServiceCollection services)
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

        // TODO: to be moved somewhere else
        #region Helpers
        private EnvironmentType GetEnvironmentType()
        {
            var environmentType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == null
                ? EnvironmentType.Production : EnvironmentType.Development;
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
        #endregion Helpers
    }
}
