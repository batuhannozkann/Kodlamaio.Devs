using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class GitHubProfileConfiguration : IEntityTypeConfiguration<GitHubProfile>
    {
        public void Configure(EntityTypeBuilder<GitHubProfile> builder)
        {
            builder.ToTable("GitHubProfiles").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.ProfileUrl).HasColumnName("ProfileUrl");
            builder.Property(p => p.ProfileName).HasColumnName("ProfileName");
            builder.Property(p => p.DeveloperId).HasColumnName("DeveloperId");
            builder.HasOne(p =>p.Developer);
        }
    }
}
