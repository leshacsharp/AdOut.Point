using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdOut.Point.DataProvider.Repositories
{
    public class AdPointDayOffRepository : BaseRepository<AdPointDayOff>, IAdPointDayOffRepository
    {
        public AdPointDayOffRepository(IDatabaseContext context)
            : base(context)
        {
        }

        public Task<AdPointDayOff> GetByIdAsync(int adPointId, int dayOffId)
        {
            return Context.AdPointsDaysOff.SingleOrDefaultAsync(apd => apd.AdPointId == adPointId && apd.DayOffId == dayOffId);
        }
    }
}
