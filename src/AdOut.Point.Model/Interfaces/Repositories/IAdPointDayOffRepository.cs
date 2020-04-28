using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface IAdPointDayOffRepository : IBaseRepository<AdPointDayOff>
    {
        Task<AdPointDayOff> GetByIdAsync(int adPointId, int dayOffId);
    }
}
