using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface IAdPointDayOffManager : IBaseManager<AdPointDayOff>
    {
        Task AddDayOffToAdPointAsync(string adPointId, string dayOffId);
        Task DeleteDayOffFromAdPointAsync(string adPointId, string dayOffId);
    }
}
