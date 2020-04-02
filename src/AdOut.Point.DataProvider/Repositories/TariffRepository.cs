using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var query = from t in Context.Tariffs
                        where t.Id == tariffId
                        select t;

            return query.SingleOrDefaultAsync();
        }
    }
}
