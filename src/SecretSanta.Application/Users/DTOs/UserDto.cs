using AutoMapper;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Users.DTOs
{
    public class UserDto
    {
        public string UserName { get; init; }
        public string Email { get; init; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
        }
    }
}
