using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Configurations;
using Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext:DbContext
    {
        IConfiguration Configuration;
        public DbSet<Language> Languages { get; set; }
        public BaseDbContext(IConfiguration configuration,DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedLanguage();
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        }

        

  
    }
}
