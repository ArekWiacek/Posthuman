using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posthuman.Core.Models.Entities;
using Posthuman.Core.Services;
using Posthuman.RealTime.Notifications;
using Posthuman.Services;
using Posthuman.Services.Helpers;

namespace Posthuman.WebApi.Installers
{
    public class CustomServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITodoItemsService, TodoItemsService>();
            services.AddTransient<IEventItemsService, EventItemsService>();
            services.AddTransient<IAvatarsService, AvatarsService>();
            services.AddTransient<IBlogPostsService, BlogPostsService>();
            services.AddTransient<ITechnologyCardsService, TechnologyCardsService>();
            services.AddTransient<IHabitsService, HabitsService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddSingleton<IExperienceHelper, ExperienceHelper>();
        }
    }
}
