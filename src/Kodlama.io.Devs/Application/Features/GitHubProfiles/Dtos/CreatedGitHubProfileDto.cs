﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Dtos
{
    public class CreatedGitHubProfileDto
    {
        public int Id { get; set; }
        public int DeveloperId { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfileName { get; set; }
        

    }
}
