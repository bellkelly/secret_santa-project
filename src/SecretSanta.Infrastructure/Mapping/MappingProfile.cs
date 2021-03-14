using AutoMapper;
using SecretSanta.Domain.Entities;
using SecretSanta.Infrastructure.Identity;

namespace SecretSanta.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, Member>();
        }
    }
}
