using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Infrastructure.Identity;
using SecretSanta.Infrastructure.Persistence;

namespace SecretSanta.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                if (context.Database.IsNpgsql())
                {
                    await context.Database.EnsureCreatedAsync();
                }

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred while migrating or seeding the database.");
                throw;
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
