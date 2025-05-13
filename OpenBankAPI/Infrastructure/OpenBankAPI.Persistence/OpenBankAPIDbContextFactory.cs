using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Factories
{
    public class OpenBankAPIDbContextFactory : IDesignTimeDbContextFactory<OpenBankAPIDbContext>
    {
        public OpenBankAPIDbContext CreateDbContext(string[] args)
        {
            var basePath = "/Users/iremnur/Desktop/Staj/OpenBankAPI/Presentation/OpenBankAPI.API";
            Console.WriteLine("Config Path: " + basePath); // DEBUG için

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MySqlConnection");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<OpenBankAPIDbContext>();
            dbContextOptionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new OpenBankAPIDbContext(dbContextOptionsBuilder.Options);
        }







        /*public OpenBankAPIDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Presentation/OpenBankAPI.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MySqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<OpenBankAPIDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new OpenBankAPIDbContext(optionsBuilder.Options);
        }*/
    }
}
