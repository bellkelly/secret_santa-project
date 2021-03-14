using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Users.Commands;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Users.Handlers
{
    /// <summary>
    /// Handle the creation of a new <see cref="User" /> given a <see cref="CreateUserCommand" />.
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserDbContext _userDbContext;

        public CreateUserCommandHandler(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User {UserName = request.UserName, Email = request.Email};
            var (result, username) = await _userDbContext.CreateAsync(user, request.Password);

            if (result.Succeeded) return username;

            throw new ValidationException();
        }
    }
}
