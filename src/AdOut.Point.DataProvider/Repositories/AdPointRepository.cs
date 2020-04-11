using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdOut.Point.DataProvider.Repositories
{
    public class AdPointRepository : BaseRepository<AdPoint>, IAdPointRepository
    {
        public AdPointRepository(IDatabaseContext context) 
            : base(context)
        {
        }

        public Task<AdPoint> GetByIdAsync(int adPointId)
        {
            return Context.AdPoints.SingleOrDefaultAsync(ap => ap.Id == adPointId);
        }
    }
}
