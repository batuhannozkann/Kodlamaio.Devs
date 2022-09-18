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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.FirstName).HasColumnName("FirstName");
            builder.Property(p => p.Status).HasColumnName("Status");
            builder.Property(p => p.Email).HasColumnName("Email");
            builder.Property(p => p.LastName).HasColumnName("LastName");
            builder.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
            builder.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
            builder.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
            builder.HasMany(p => p.RefreshTokens);
            builder.HasMany(p => p.UserOperationClaims);
        }
    }
}
