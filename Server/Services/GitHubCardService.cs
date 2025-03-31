using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using RyanJagdfeld.Module.GitHubCard.Repository;

namespace RyanJagdfeld.Module.GitHubCard.Services
{
    public class ServerGitHubCardService : IGitHubCardService
    {
        private readonly IGitHubCardRepository _GitHubCardRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerGitHubCardService(IGitHubCardRepository GitHubCardRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _GitHubCardRepository = GitHubCardRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.GitHubCard>> GetGitHubCardsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_GitHubCardRepository.GetGitHubCards(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.GitHubCard> GetGitHubCardAsync(int GitHubCardId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_GitHubCardRepository.GetGitHubCard(GitHubCardId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Get Attempt {GitHubCardId} {ModuleId}", GitHubCardId, ModuleId);
                return null;
            }
        }

        public Task<Models.GitHubCard> AddGitHubCardAsync(Models.GitHubCard GitHubCard)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, GitHubCard.ModuleId, PermissionNames.Edit))
            {
                GitHubCard = _GitHubCardRepository.AddGitHubCard(GitHubCard);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "GitHubCard Added {GitHubCard}", GitHubCard);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Add Attempt {GitHubCard}", GitHubCard);
                GitHubCard = null;
            }
            return Task.FromResult(GitHubCard);
        }

        public Task<Models.GitHubCard> UpdateGitHubCardAsync(Models.GitHubCard GitHubCard)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, GitHubCard.ModuleId, PermissionNames.Edit))
            {
                GitHubCard = _GitHubCardRepository.UpdateGitHubCard(GitHubCard);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "GitHubCard Updated {GitHubCard}", GitHubCard);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Update Attempt {GitHubCard}", GitHubCard);
                GitHubCard = null;
            }
            return Task.FromResult(GitHubCard);
        }

        public Task DeleteGitHubCardAsync(int GitHubCardId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _GitHubCardRepository.DeleteGitHubCard(GitHubCardId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "GitHubCard Deleted {GitHubCardId}", GitHubCardId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Delete Attempt {GitHubCardId} {ModuleId}", GitHubCardId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
