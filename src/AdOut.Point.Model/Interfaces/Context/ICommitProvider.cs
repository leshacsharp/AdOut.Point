using System.Threading;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Context
{
    public interface ICommitProvider
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
