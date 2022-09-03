using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedLanguage(this ModelBuilder builder)
        {
            builder.Entity<Language>().HasData(
                new Language(1, "C#"), new Language(2, "Python"), new Language(3, "Java")
           );
        }
    }
}
