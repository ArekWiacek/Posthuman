using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posthuman.Core;
using Posthuman.Core.Repositories;
using Posthuman.Data;
using Posthuman.Data.Repositories;

namespace Posthuman.WebApi.Installers
{
    public class CustomRepositoriesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
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
    }
}
