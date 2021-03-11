using System.Linq;
using Microsoft.AspNetCore.Identity;
using SecretSanta.Application.Common.Models;

namespace SecretSanta.Infrastructure.Identity
{
    /// <summary>
    /// A wrapper around <see cref="IdentityResult" />
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Return <see cref="Result.Success" /> if result succeeded, else return <see cref="Result.Failure" />.
        /// </summary>
        /// <param name="result">An <see cref="IdentityResult" /></param>
        /// <returns></returns>
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
