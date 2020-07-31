using AdOut.Point.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Context
{
    public interface IDatabaseContext
    {
        DbSet<AdPoint> AdPoints { get; set; }

        DbSet<Tariff> Tariffs { get; set; }

        DbSet<DayOff> DaysOff { get; set; }

        DbSet<AdPointDayOff> AdPointsDaysOff { get; set; }

        DbSet<Image> Images { get; set; }

        DbSet<ComputerState> ComputerStates { get; set; }

        DbSet<Plan> Plans { get; set; }

        DbSet<PlanAdPoint> AdPointPlans { get; set; }

        ChangeTracker ChangeTracker { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
