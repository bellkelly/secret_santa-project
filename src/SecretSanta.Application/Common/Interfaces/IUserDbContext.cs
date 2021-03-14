using System.Threading.Tasks;
using SecretSanta.Application.Common.Models;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Common.Interfaces
{
    public interface IUserDbContext
    {
        public Task<User> FindByEmailAsync(string email);
        public Task<User> FindByUsernameAsync(string userName);
        public Task<bool> CheckPassword(User user, string password);
        public Task ChangePassword(User user, string password);
        public Task<(Result Result, string Username)> CreateAsync(User user, string password);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}
