using MediatR;
using SecretSanta.Application.Members.DTOs;

namespace SecretSanta.Application.Members.Queries.GetMember
{
    public class GetMemberQuery : IRequest<MemberDto>
    {
        public string UserName { get; set; }
    }
}
