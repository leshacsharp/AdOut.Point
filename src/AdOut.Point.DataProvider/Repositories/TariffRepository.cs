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
        public Task<List<PlanTariffDto>> GetAdPointTariffsAsync(string planId)
        {
            var q = Context.AdPointPlans.Where(app => app.PlanId == planId)
                                        .Select(app => new PlanTariffDto()
                                        {
                                            AdPointId = app.AdPointId,
                                            Tariffs = app.AdPoint.Tariffs.Select(t => new TariffDto()
                                            {
                                                StartTime = t.StartTime,
                                                EndTime = t.EndTime,
                                                PriceForMinute = t.PriceForMinute
                                            })
                                        });

            return q.ToListAsync();
        }
    }
}
