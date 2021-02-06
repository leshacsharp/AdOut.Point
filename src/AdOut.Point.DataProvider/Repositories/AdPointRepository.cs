using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace AdOut.Point.DataProvider.Repositories
{
    public class AdPointRepository : BaseRepository<AdPoint>, IAdPointRepository
    {
        public AdPointRepository(IDatabaseContext context) 
            : base(context)
        {
        }

        public Task<AdPointDto> GetDtoByIdAsync(string adPointId)
        {
            var query = Context.AdPoints.Where(ap => ap.Id == adPointId)
                                        .Select(ap => new AdPointDto()
                                        {
                                            Location = ap.Location,
                                            StartWorkingTime = ap.StartWorkingTime,
                                            EndWorkingTime = ap.EndWorkingTime,
                                            ScreenWidthCm = ap.ScreenWidthCm,
                                            ScreenHeightCm = ap.ScreenHeightCm,
                                            Images = ap.Images.Select(i => i.Path),
                                            AdPointsDaysOff = ap.AdPointsDaysOff.Select(apd => apd.DayOff.DayOfWeek),
                                            Plans = ap.PlanAdPoints.Select(pap => new PlanDto()
                                            { 
                                                Title = pap.Plan.Title
                                            }),
                                            Tariffs = ap.Tariffs.Select(t => new TariffDto()
                                            {
                                                StartTime = t.StartTime,
                                                EndTime = t.EndTime,
                                                PriceForMinute = t.PriceForMinute
                                            })
                                        });

            return query.SingleOrDefaultAsync();
        }
    }
}
