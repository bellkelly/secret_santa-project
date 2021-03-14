using AutoMapper;
using SecretSanta.Application.Users.DTOs;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
