using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.DataProvider.Repositories
{
    public class DayOffRepository : BaseRepository<DayOff>, IDayOffRepository
    {
        public DayOffRepository(IDatabaseContext context) 
            : base(context)
        {
        }
    }
}
