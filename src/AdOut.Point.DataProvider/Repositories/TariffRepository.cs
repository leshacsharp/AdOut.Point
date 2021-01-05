using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.DataProvider.Repositories
{
    public class TariffRepository : BaseRepository<Tariff>, ITariffRepository
    {
        public TariffRepository(IDatabaseContext context)
            : base(context)
        {
        }
    }
}
