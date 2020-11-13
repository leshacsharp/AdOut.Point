using AdOut.Point.Model.Classes;
using AdOut.Point.Model.Dto;
using AdOut.Point.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Managers
{
    public class PlanManager
    {
        private readonly IPlanRepository _planRepository;
        private readonly IPlanAdPointRepository _planAdPointRepository;
        private readonly ITariffRepository _tariffRepository;

        public PlanManager(
            IPlanRepository planRepository,
            IPlanAdPointRepository planAdPointRepository,
            ITariffRepository tariffRepository
            )
        {
            _planRepository = planRepository;
            _planAdPointRepository = planAdPointRepository;
            _tariffRepository = tariffRepository;
        }

        //todo: need to be done
        //todo: need to put arguments and move this logic to RabbitMQ consumer
        public async Task CalculatePlanPriceAsync()
        {
            var planId = Guid.NewGuid().ToString();
            var planStartTime = DateTime.UtcNow.AddDays(-10);
            var planEndTime = DateTime.UtcNow;
            var adPeriods = new List<AdPeriod>();

            var adPointTariffs = await _tariffRepository.GetAdPointTariffsAsync(planId);
            var planPrice = 0d;

            foreach (var apTariffs in adPointTariffs)
            {
                foreach (var t in apTariffs.Tariffs)
                {
                    var tariffAdPeriods = adPeriods.Where(ap => ap.StartTime >= t.StartTime && ap.EndTime <= t.EndTime).ToList();
                    var adsPlayingTimeSec = tariffAdPeriods.Select(ap => new { PlayTime = (ap.EndTime - ap.StartTime).TotalSeconds }).Sum(s => s.PlayTime);
                    var priceOfAdPeriodsTime = adsPlayingTimeSec * (t.PriceForMinute / 60);

                    planPrice += priceOfAdPeriodsTime;
                }
            }
        }
    }
}
