using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

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
            SeedData(modelBuilder);

            modelBuilder.Entity<AdPointDayOff>()
                        .HasKey(apd => new { apd.AdPointId, apd.DayOffId });

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var daysOff = new List<DayOff>();
            var daysOfWeek = Enum.GetValues(typeof(DayOfWeek));

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                var dayOff = new DayOff()
                {
                    Id = i + 1,
                    DayOfWeek = (DayOfWeek)i
                };

                daysOff.Add(dayOff);
            }

            modelBuilder.Entity<DayOff>().HasData(daysOff);
        }
    }
}
