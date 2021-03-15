using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SecretSanta.Application.Common.Exceptions;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Users.DTOs;
using SecretSanta.Application.Users.Queries;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Users.Handlers
{
    /// <summary>
    /// Handle fetching a <see cref="User" /> given a <see cref="GetUserQuery" />.
    /// </summary>
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserDbContext _userDbContext;

        public GetUserHandler(IUserDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userDbContext.FindByUsernameAsync(request.UserName);

            if (user == null) throw new NotFoundException(nameof(User), request.UserName);

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
