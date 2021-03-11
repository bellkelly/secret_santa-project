using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Common.Models;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Infrastructure.Identity
{
    public class IdentityService : IMemberService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<Member> GetMemberAsync(string userName)
        {
            return await _userManager.Users.Where(u => u.UserName == userName)
                .ProjectTo<Member>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<(Result Result, string UserId)> CreateMemberAsync(string userName, string password)
        {
            var user = new ApplicationUser {UserName = userName, Email = userName};
            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        /// <inheritdoc />
        public async Task<bool> MemberExists(string userName, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == userName,
                cancellationToken);

            return user != null;
        }
    }
}
