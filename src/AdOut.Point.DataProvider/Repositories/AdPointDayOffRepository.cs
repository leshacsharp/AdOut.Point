using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.DataProvider.Repositories
{
    public class AdPointDayOffRepository : BaseRepository<AdPointDayOff>, IAdPointDayOffRepository
    {
        public AdPointDayOffRepository(IDatabaseContext context)
            : base(context)
        {
        }
    }
}
