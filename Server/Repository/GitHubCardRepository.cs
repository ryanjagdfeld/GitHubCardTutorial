using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace RyanJagdfeld.Module.GitHubCard.Repository
{
    public class GitHubCardRepository : IGitHubCardRepository, ITransientService
    {
        private readonly IDbContextFactory<GitHubCardContext> _factory;

        public GitHubCardRepository(IDbContextFactory<GitHubCardContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.GitHubCard> GetGitHubCards(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.GitHubCard.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.GitHubCard GetGitHubCard(int GitHubCardId)
        {
            return GetGitHubCard(GitHubCardId, true);
        }

        public Models.GitHubCard GetGitHubCard(int GitHubCardId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.GitHubCard.Find(GitHubCardId);
            }
            else
            {
                return db.GitHubCard.AsNoTracking().FirstOrDefault(item => item.GitHubCardId == GitHubCardId);
            }
        }

        public Models.GitHubCard AddGitHubCard(Models.GitHubCard GitHubCard)
        {
            using var db = _factory.CreateDbContext();
            db.GitHubCard.Add(GitHubCard);
            db.SaveChanges();
            return GitHubCard;
        }

        public Models.GitHubCard UpdateGitHubCard(Models.GitHubCard GitHubCard)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(GitHubCard).State = EntityState.Modified;
            db.SaveChanges();
            return GitHubCard;
        }

        public void DeleteGitHubCard(int GitHubCardId)
        {
            using var db = _factory.CreateDbContext();
            Models.GitHubCard GitHubCard = db.GitHubCard.Find(GitHubCardId);
            db.GitHubCard.Remove(GitHubCard);
            db.SaveChanges();
        }
    }
}
