using Microsoft.Extensions.Configuration;
using System;

namespace Posthuman.WebApi.Utilities
{
    public class EnvironmentHelper : IEnvironmentHelper
    {
        private readonly IConfiguration configuration;

        public EnvironmentHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public EnvironmentType GetEnvironmentType()
        {
            var environmentType = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == null
                ? EnvironmentType.Production : EnvironmentType.Development;
            return environmentType;
        }

        public string GetConnectionString(EnvironmentType environmentType)
        {
            string? dbConnectionString;

            if (environmentType == EnvironmentType.Development)
                dbConnectionString = configuration.GetConnectionString("PosthumanDatabaseDevelopment");
            else
                dbConnectionString = configuration.GetConnectionString("PosthumanDatabaseProduction");

            return dbConnectionString;
        }

        public string GetFrontendUrl(EnvironmentType environmentType)
        {
            string? hostUrl;

            if (environmentType == EnvironmentType.Development)
                hostUrl = configuration.GetSection("FrontendUrl").GetValue<string>("Development");
            else
                hostUrl = configuration.GetSection("FrontendUrl").GetValue<string>("Production");

            return hostUrl;
        }

        public string GetBackendUrl(EnvironmentType environmentType)
        {
            string? hostUrl;

            if (environmentType == EnvironmentType.Development)
                hostUrl = configuration.GetSection("BackendUrl").GetValue<string>("Development");
            else
                hostUrl = configuration.GetSection("BackendUrl").GetValue<string>("Production");

            return hostUrl;
        }
    }
}
