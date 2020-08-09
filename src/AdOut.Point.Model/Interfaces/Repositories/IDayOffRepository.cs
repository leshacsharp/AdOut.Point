using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface IDayOffRepository : IBaseRepository<DayOff>
    {
        Task<DayOff> GetByIdAsync(string dayOffId);
    }
}
