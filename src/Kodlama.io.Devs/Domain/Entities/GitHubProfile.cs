using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GitHubProfile:Entity
    {
        public GitHubProfile()
        {

        }

        public GitHubProfile(string profileName, string profileUrl, int developerId) : this()
        {
            ProfileName = profileName;
            ProfileUrl = profileUrl;
            DeveloperId = developerId;
        }

        public string ProfileName { get; set; }
        public string ProfileUrl { get; set; }
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }
    }
}
