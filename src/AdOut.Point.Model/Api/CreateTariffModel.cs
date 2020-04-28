using System;

namespace AdOut.Point.Model.Api
{
    public class CreateTariffModel
    {
        public int AdPointId { get; set; }

        public double PriceForMinute { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
