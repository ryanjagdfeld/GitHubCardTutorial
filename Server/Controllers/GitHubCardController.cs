using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using RyanJagdfeld.Module.GitHubCard.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace RyanJagdfeld.Module.GitHubCard.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class GitHubCardController : ModuleControllerBase
    {
        private readonly IGitHubCardService _GitHubCardService;

        public GitHubCardController(IGitHubCardService GitHubCardService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _GitHubCardService = GitHubCardService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.GitHubCard>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _GitHubCardService.GetGitHubCardsAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.GitHubCard> Get(int id, int moduleid)
        {
            Models.GitHubCard GitHubCard = await _GitHubCardService.GetGitHubCardAsync(id, moduleid);
            if (GitHubCard != null && IsAuthorizedEntityId(EntityNames.Module, GitHubCard.ModuleId))
            {
                return GitHubCard;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Get Attempt {GitHubCardId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.GitHubCard> Post([FromBody] Models.GitHubCard GitHubCard)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, GitHubCard.ModuleId))
            {
                GitHubCard = await _GitHubCardService.AddGitHubCardAsync(GitHubCard);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Post Attempt {GitHubCard}", GitHubCard);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                GitHubCard = null;
            }
            return GitHubCard;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.GitHubCard> Put(int id, [FromBody] Models.GitHubCard GitHubCard)
        {
            if (ModelState.IsValid && GitHubCard.GitHubCardId == id && IsAuthorizedEntityId(EntityNames.Module, GitHubCard.ModuleId))
            {
                GitHubCard = await _GitHubCardService.UpdateGitHubCardAsync(GitHubCard);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Put Attempt {GitHubCard}", GitHubCard);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                GitHubCard = null;
            }
            return GitHubCard;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.GitHubCard GitHubCard = await _GitHubCardService.GetGitHubCardAsync(id, moduleid);
            if (GitHubCard != null && IsAuthorizedEntityId(EntityNames.Module, GitHubCard.ModuleId))
            {
                await _GitHubCardService.DeleteGitHubCardAsync(id, GitHubCard.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GitHubCard Delete Attempt {GitHubCardId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
