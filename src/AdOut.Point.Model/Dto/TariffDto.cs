using System;

namespace AdOut.Point.Model.Dto
{
    public class TariffDto
    {
        public double PriceForMin { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
