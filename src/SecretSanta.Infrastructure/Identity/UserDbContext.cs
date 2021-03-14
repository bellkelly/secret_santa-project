using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SecretSanta.Application.Common.Interfaces;
using SecretSanta.Application.Common.Models;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Infrastructure.Identity
{
    public class UserDbContext : IUserDbContext
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserDbContext(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return _mapper.Map<User>(user);
        }

        public async Task<User> FindByUsernameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return _mapper.Map<User>(user);
        }

        public async Task<bool> CheckPassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task ChangePassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<(Result Result, string Username)> CreateAsync(User user, string password)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            var result = await _userManager.CreateAsync(appUser, password);

            return (result.ToApplicationResult(), appUser.UserName);
        }

        public async Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
