using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class OtpAuthenticatorConfiguration : IEntityTypeConfiguration<OtpAuthenticator>
    {
        public void Configure(EntityTypeBuilder<OtpAuthenticator> builder)
        {
            builder.ToTable("OtpAuthenticators").HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.Property(p => p.SecretKey).HasColumnName("SecretKey");
            builder.Property(p => p.IsVerified).HasColumnName("IsVerified");

            builder.HasOne(p => p.User);
        }
    }
}
