using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Infrastructure.Identity
{
    /// <summary>
    /// A User in the system.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<User, ApplicationUser>();
        }
    }
}
