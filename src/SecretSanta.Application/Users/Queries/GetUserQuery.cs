using MediatR;
using SecretSanta.Application.Users.DTOs;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Users.Queries
{
    /// <summary>
    /// A query used for fetching an existing <see cref="User" />.
    /// </summary>
    public class GetUserQuery : IRequest<UserDto>
    {
        public string UserName { get; set; }
    }
}
