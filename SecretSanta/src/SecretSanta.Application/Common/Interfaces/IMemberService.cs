using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Application.Common.Models;
using SecretSanta.Domain.Entities;

namespace SecretSanta.Application.Common.Interfaces
{
    /// <summary>
    /// An interface for interacting with <see cref="Member" />s.
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Return a <see cref="Member" /> with the given id.
        /// </summary>
        /// <param name="userName">The username of the <see cref="Member" /> to lookup.</param>
        /// <returns></returns>
        Task<Member> GetMemberAsync(string userName);

        /// <summary>
        /// Create a new <see cref="Member" /> and return the UserId.
        /// </summary>
        /// <param name="userName">A string representing the <see cref="Member" />'s username.</param>
        /// <param name="password">A string representing the <see cref="Member" />'s password.</param>
        /// <returns></returns>
        Task<(Result Result, string UserId)> CreateMemberAsync(string userName, string password);

        /// <summary>
        /// Return True if a <see cref="Member" /> exists with the given userName.
        /// </summary>
        /// <param name="userName">A string representing the <see cref="Member" />'s username.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns></returns>
        Task<bool> MemberExists(string userName, CancellationToken cancellationToken);
    }
}
