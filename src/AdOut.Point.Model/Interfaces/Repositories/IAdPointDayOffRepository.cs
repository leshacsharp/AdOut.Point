using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface IAdPointDayOffRepository : IBaseRepository<AdPointDayOff>
    {
        Task<AdPointDayOff> GetByIdAsync(string adPointId, string dayOffId);
    }
}
