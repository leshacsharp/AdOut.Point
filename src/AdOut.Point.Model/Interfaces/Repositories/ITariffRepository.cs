using AdOut.Point.Model.Database;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface ITariffRepository : IBaseRepository<Tariff>
    {
        Task<Tariff> GetByIdAsync(string tariffId);
    }
}
