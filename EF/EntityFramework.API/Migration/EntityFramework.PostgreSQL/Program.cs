using EntityFramework.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EntityFramework.PostgreSQL
{
    internal class Program : IDesignTimeDbContextFactory<EFContext>
    {
        public EFContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("database.json")
                               .Build();

            return new EFContext(configuration);
        }
    }
}
