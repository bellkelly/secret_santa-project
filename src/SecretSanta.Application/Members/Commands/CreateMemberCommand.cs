using MediatR;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Members.Commands
{
    /// <summary>
    /// A command use for creating a new <see cref="Member" />.
    /// </summary>
    public class CreateMemberCommand : IRequest<string>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
