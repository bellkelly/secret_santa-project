using System;
using AutoMapper;
using SecretSanta.Domain.Entities;
using SecretSanta.Infrastructure.Identity;

namespace SecretSanta.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, User>();
            CreateMap<User, ApplicationUser>().ForMember(x => x.Id, opt => opt.MapFrom(o => Guid.NewGuid()));
        }
    }
}
