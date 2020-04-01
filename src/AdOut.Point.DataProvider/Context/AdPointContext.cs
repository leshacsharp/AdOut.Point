using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace AdOut.Point.DataProvider.Context
{
    public class AdPointContext : DbContext, IDatabaseContext
    {
        public AdPointContext(DbContextOptions<AdPointContext> dbContextOptions) 
            : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<AdPoint> AdPoints { get; set; }

        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<DayOff> DaysOff { get; set; }

        public DbSet<AdPointDayOff> AdPointsDaysOff { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ComputerState> ComputerStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AdPointDayOff>()
                        .HasKey(apd => new { apd.AdPointId, apd.DayOffId }); 
        }
    }
}
