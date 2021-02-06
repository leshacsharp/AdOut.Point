using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.DataProvider.Repositories
{
    public class PlanAdPointRepository : BaseRepository<PlanAdPoint>, IPlanAdPointRepository
    {
        public PlanAdPointRepository(IDatabaseContext context)
            : base(context)
        {
        }
    }
}
