using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var query = from ap in Context.AdPoints
                        where ap.Id == adPointId
                        select ap;

            return query.SingleOrDefaultAsync();
        }
    }
}
