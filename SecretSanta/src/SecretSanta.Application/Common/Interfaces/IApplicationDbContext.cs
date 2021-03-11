using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
