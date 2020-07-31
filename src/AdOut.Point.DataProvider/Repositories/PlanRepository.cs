using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;

namespace AdOut.Point.DataProvider.Repositories
{
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(IDatabaseContext context)
            : base(context)
        {
        }
    }
}
