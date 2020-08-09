using System;

namespace AdOut.Point.Model.Api
{
    public class UpdateTariffModel
    {
        public string TariffId { get; set; }

        public string AdPointId { get; set; }

        public double PriceForMinute { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
