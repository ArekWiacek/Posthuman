using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posthuman.Data;
using System;

namespace Posthuman.WebApi.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var environmentType = GetEnvironmentType();
            var connectionString = GetConnectionString(environmentType, configuration);
            
            services
                .AddDbContext<PosthumanContext>(options => options
                    .UseSqlServer(connectionString,
                        x => x.MigrationsAssembly("Posthuman.Data")));
        }

        private string GetConnectionString(EnvironmentType environmentType, IConfiguration configuration)
        {
            string? dbConnectionString;

            if (environmentType == EnvironmentType.Development)
                dbConnectionString = configuration.GetConnectionString("PosthumanDatabaseDevelopment");
            else
                dbConnectionString = configuration.GetConnectionString("PosthumanDatabaseProduction");

            return dbConnectionString;
        }

        private EnvironmentType GetEnvironmentType()
        {
            var environmentType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == null
                ? EnvironmentType.Production : EnvironmentType.Development;
            return environmentType;
        }
    }
}
