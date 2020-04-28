using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface IAdPointDayOffManager : IBaseManager<AdPointDayOff>
    {
        Task AddDayOffToAdPointAsync(int adPointId, int dayOffId);
        Task DeleteDayOffFromAdPoint(int adPointId, int dayOffId);
    }
}
