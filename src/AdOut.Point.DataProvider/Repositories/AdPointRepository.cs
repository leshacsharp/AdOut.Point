using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace AdOut.Point.DataProvider.Repositories
{
    public class AdPointRepository : BaseRepository<AdPoint>, IAdPointRepository
    {
        public AdPointRepository(IDatabaseContext context) 
            : base(context)
        {
        }

        public async Task<AdPointDto> GetDtoByIdAsync(int adPointId)
        {
            var query = from ap in Context.AdPoints

                        join t in Context.Tariffs on ap.Id equals t.AdPointId into tJoin
                        from t in tJoin.DefaultIfEmpty()

                        join i in Context.Images on ap.Id equals i.AdPointId into iJoin
                        from i in iJoin.DefaultIfEmpty()

                        join apd in Context.AdPointsDaysOff on ap.Id equals apd.AdPointId into apdJoin
                        from apd in apdJoin.DefaultIfEmpty()

                        join d in Context.DaysOff on apd.DayOffId equals d.Id into dJoin
                        from d in dJoin.DefaultIfEmpty()

                        where ap.Id == adPointId

                        select new
                        {
                            ap.Location,
                            ap.StartWorkingTime,
                            ap.EndWorkingTime,
                            ap.ScreenWidthCm,
                            ap.ScreenHeightCm,

                            Image = i != null ? i.Path : null,
                            DayOff = d != null ? d.DayOfWeek : (DayOfWeek?)null,
                            Tariff = t != null ? new TariffDto()
                            {
                                StartTime = t.StartTime,
                                EndTime = t.EndTime,
                                PriceForMinute = t.PriceForMinute
                            } : null
                        };

            var adPointItems = await query.ToListAsync();
            var adPoint = adPointItems.SingleOrDefault();

            var result = adPoint != null ? new AdPointDto()
            {
                Location = adPoint.Location,
                StartWorkingTime = adPoint.StartWorkingTime,
                EndWorkingTime = adPoint.EndWorkingTime,
                ScreenWidthCm = adPoint.ScreenWidthCm,
                ScreenHeightCm = adPoint.ScreenHeightCm,

                Images = adPointItems.Where(ap => ap.Image != null).Select(ap => ap.Image),
                AdPointsDaysOff = adPointItems.Where(ap => ap.DayOff != null).Select(ap => ap.DayOff.Value),
                Tariffs = adPointItems.Where(ap => ap.Tariff != null).Select(ap => ap.Tariff)
            } : null;

            return result;
        }

        public Task<AdPoint> GetByIdAsync(int adPointId)
        {
            return Context.AdPoints.SingleOrDefaultAsync(ap => ap.Id == adPointId);
        }   
    }
}
