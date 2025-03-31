using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using RyanJagdfeld.Module.GitHubCard.Services;

namespace RyanJagdfeld.Module.GitHubCard.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGitHubCardService, GitHubCardService>();
            services.AddScoped<IGitHubService, GitHubService>();
        }
    }
}
