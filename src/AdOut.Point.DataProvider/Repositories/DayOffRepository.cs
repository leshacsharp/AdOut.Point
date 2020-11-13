using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdOut.Point.DataProvider.Repositories
{
    public class DayOffRepository : BaseRepository<DayOff>, IDayOffRepository
    {
        public DayOffRepository(IDatabaseContext context) 
            : base(context)
        {
        }

        public Task<DayOff> GetByIdAsync(string dayOffId)
        {
            return Context.DaysOff.SingleOrDefaultAsync(d => d.Id == dayOffId);
        }
    }
}
