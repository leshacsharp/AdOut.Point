using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface IAdPointDayOffManager
    {
        Task AddDayOffToAdPointAsync(string adPointId, string dayOffId);
        Task DeleteDayOffFromAdPointAsync(string adPointId, string dayOffId);
    }
}
