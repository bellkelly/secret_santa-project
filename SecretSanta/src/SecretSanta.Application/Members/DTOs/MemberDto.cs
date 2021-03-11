using AutoMapper;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Members.DTOs
{
    public class MemberDto
    {
        public string UserName { get; init; }
        public string Email { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Member, MemberDto>();
        }
    }
}
