using confusionresturant.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace confusionresturant.Data
{
    public class CrContext : IdentityDbContext
    {    
        private IConfigurationRoot _config;
        public CrContext(DbContextOptions<CrContext> options,
         IConfigurationRoot config)
        :base(options)
        { 
            _config = config;     
        }
        public DbSet<Dish> Dishes {get; set;}
        public DbSet<Comment> Comments {get; set;}
        
        protected override void  OnConfiguring(DbContextOptionsBuilder 
        optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }
    }
}