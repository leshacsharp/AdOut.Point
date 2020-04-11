using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdOut.Point.DataProvider.Repositories
{
    public class TariffRepository : BaseRepository<Tariff>, ITariffRepository
    {
        public TariffRepository(IDatabaseContext context)
            : base(context)
        {
        }

        public Task<Tariff> GetByIdAsync(int tariffId)
        {
            return Context.Tariffs.SingleOrDefaultAsync(t => t.Id == tariffId);
        }
    }
}
