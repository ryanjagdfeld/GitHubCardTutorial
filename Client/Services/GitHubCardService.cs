using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace RyanJagdfeld.Module.GitHubCard.Services
{
    public class GitHubCardService : ServiceBase, IGitHubCardService
    {
        public GitHubCardService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("GitHubCard");

        public async Task<List<Models.GitHubCard>> GetGitHubCardsAsync(int ModuleId)
        {
            List<Models.GitHubCard> GitHubCards = await GetJsonAsync<List<Models.GitHubCard>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.GitHubCard>().ToList());
            return GitHubCards.OrderBy(item => item.Username).ToList();
        }

        public async Task<Models.GitHubCard> GetGitHubCardAsync(int GitHubCardId, int ModuleId)
        {
            return await GetJsonAsync<Models.GitHubCard>(CreateAuthorizationPolicyUrl($"{Apiurl}/{GitHubCardId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.GitHubCard> AddGitHubCardAsync(Models.GitHubCard GitHubCard)
        {
            return await PostJsonAsync<Models.GitHubCard>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, GitHubCard.ModuleId), GitHubCard);
        }

        public async Task<Models.GitHubCard> UpdateGitHubCardAsync(Models.GitHubCard GitHubCard)
        {
            return await PutJsonAsync<Models.GitHubCard>(CreateAuthorizationPolicyUrl($"{Apiurl}/{GitHubCard.GitHubCardId}", EntityNames.Module, GitHubCard.ModuleId), GitHubCard);
        }

        public async Task DeleteGitHubCardAsync(int GitHubCardId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{GitHubCardId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
