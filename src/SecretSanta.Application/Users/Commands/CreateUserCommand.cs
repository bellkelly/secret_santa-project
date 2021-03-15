using MediatR;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Users.Commands
{
    /// <summary>
    /// A command used for creating a new <see cref="User" />.
    /// </summary>
    public class CreateUserCommand : IRequest<string>
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
