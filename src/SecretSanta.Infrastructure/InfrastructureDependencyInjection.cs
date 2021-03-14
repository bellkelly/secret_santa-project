using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Infrastructure.Identity;
using SecretSanta.Infrastructure.Persistence;

namespace SecretSanta.Infrastructure
{
    /// <summary>
    /// Inject service configuration for the Infrastructure layer.
    /// </summary>
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDefaultIdentity<ApplicationUser>(options => { options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = null;
                    options.SignIn.RequireConfirmedEmail = true;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IUserDbContext, UserDbContext>();

            services.AddAuthentication().AddIdentityServerJwt();

            return services;
        }
    }
}
