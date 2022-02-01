namespace Posthuman.WebApi.Utilities
{
    public interface IEnvironmentHelper
    {
        EnvironmentType GetEnvironmentType();
        string GetConnectionString(EnvironmentType environmentType);
        string GetFrontendUrl(EnvironmentType environmentType);
        string GetBackendUrl(EnvironmentType environmentType);
    }
}
