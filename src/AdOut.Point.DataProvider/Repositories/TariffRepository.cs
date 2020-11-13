using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Context;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using AdOut.Point.Model.Dto;

namespace AdOut.Point.DataProvider.Repositories
{
    public class TariffRepository : BaseRepository<Tariff>, ITariffRepository
    {
        public TariffRepository(IDatabaseContext context)
            : base(context)
        {
        }

        public Task<Tariff> GetByIdAsync(string tariffId)
        {
            return Context.Tariffs.SingleOrDefaultAsync(t => t.Id == tariffId);
        }

        //todo: wtf with name?
        public async Task<List<PlanTariffDto>> GetAdPointTariffsAsync(string planId)
        {
            //todo: maybe we need a left join
            var query = from app in Context.AdPointPlans.Where(app => app.PlanId == planId)
                        join t in Context.Tariffs on app.AdPointId equals t.AdPointId
                        select new
                        {
                            app.AdPointId,
                            Tariff = new TariffDto()
                            {
                                StartTime = t.StartTime,
                                EndTime = t.EndTime,
                                PriceForMinute = t.PriceForMinute
                            }
                        };

            var tariffs = await query.ToListAsync();

            var result = from t in tariffs
                         group t by t.AdPointId 
                         into grTariffs

                         select new PlanTariffDto()
                         {
                             AdPointId = grTariffs.Key,
                             Tariffs = grTariffs.Select(t => t.Tariff).ToList()
                         };

            return result.ToList();
        }
    }
}
