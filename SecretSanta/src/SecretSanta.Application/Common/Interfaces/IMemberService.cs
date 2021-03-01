using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Common.Interfaces
{
    public interface IMemberService
    {
        Member GetMember(string id);
        bool CreateMember(Member member);
    }
}