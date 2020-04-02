using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface IAdPointRepository : IBaseRepository<AdPoint>
    {
        Task<AdPoint> GetByIdAsync(int adPointId);
    }
}
