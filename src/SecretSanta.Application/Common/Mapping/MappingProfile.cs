using AutoMapper;
using SecretSanta.Application.Members.DTOs;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, MemberDto>();
        }
    }
}
