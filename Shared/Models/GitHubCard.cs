using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace RyanJagdfeld.Module.GitHubCard.Models
{
    [Table("RyanJagdfeldGitHubCard")]
    public class GitHubCard : IAuditable
    {
        [Key]
        public int GitHubCardId { get; set; }
        public int ModuleId { get; set; }
        public string Username { get; set; }
        [NotMapped]
        public GitHubUser GitHubUser { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
