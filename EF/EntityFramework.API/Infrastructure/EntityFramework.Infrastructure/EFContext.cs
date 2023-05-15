using EntityFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFramework.Infrastructure
{
    public class EFContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public EFContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"), 
                                     builder => builder.MigrationsAssembly("EntityFramework.PostgreSQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}