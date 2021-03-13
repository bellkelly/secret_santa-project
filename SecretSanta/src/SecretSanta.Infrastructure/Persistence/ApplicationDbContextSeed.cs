using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SecretSanta.Infrastructure.Identity;

namespace SecretSanta.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var administrator = new ApplicationUser { UserName = "admin", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Admin123!");
            }
        }
    }
}
