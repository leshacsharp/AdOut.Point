using System;

namespace AdOut.Point.Model.Api
{
    public class UpdateTariffModel
    {
        public int TariffId { get; set; }

        public int AdPointId { get; set; }

        public double PriceForMin { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
