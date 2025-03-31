using RyanJagdfeld.Module.GitHubCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanJagdfeld.Module.GitHubCard.Services
{
    public interface IGitHubService
    {
        Task<GitHubUser> GetGitHubUserAsync(string username);
    }
}
