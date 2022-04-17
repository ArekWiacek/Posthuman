using Microsoft.Extensions.DependencyInjection;
using Posthuman.Core;
using Posthuman.Core.Repositories;
using Posthuman.Core.Services;
using Posthuman.Data;
using Posthuman.Data.Repositories;
using Posthuman.Services;
using Posthuman.RealTime.Notifications;
using Microsoft.AspNetCore.Identity;
using Posthuman.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

namespace Posthuman.WebApi.Extensions
{
    public static class StartupServiceExtensions
    {
        public static void AddDotnetServices(this IServiceCollection services, string connectionString)
        {
            //var environmentType = GetEnvironmentType();

            services
                .AddDbContext<PosthumanContext>(options => options
                    .UseSqlServer(connectionString,
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

        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITodoItemsRepository, TodoItemsRepository>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();
            services.AddTransient<IEventItemsRepository, EventItemsRepository>();
            services.AddTransient<IAvatarsRepository, AvatarsRepository>();
            services.AddTransient<IBlogPostsRepository, BlogPostsRepository>();
            services.AddTransient<ITechnologyCardsRepository, TechnologyCardsRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITodoItemsService, TodoItemsService>();
            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IEventItemsService, EventItemsService>();
            services.AddTransient<IAvatarsService, AvatarsService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();
            services.AddTransient<ITechnologyCardsService, TechnologyCardsService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}
