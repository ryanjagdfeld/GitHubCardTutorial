using Oqtane.Models;
using Oqtane.Modules;

namespace RyanJagdfeld.Module.GitHubCard
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "GitHubCard",
            Description = "An Oqtane module extension for displaying a GitHub user information",
            Version = "1.0.0",
            ServerManagerType = "RyanJagdfeld.Module.GitHubCard.Manager.GitHubCardManager, RyanJagdfeld.Module.GitHubCard.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "RyanJagdfeld.Module.GitHubCard.Shared.Oqtane",
            PackageName = "RyanJagdfeld.Module.GitHubCard" 
        };
    }
}
