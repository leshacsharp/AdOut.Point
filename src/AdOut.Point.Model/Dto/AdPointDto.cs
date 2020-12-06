using System;
using System.Collections.Generic;

namespace AdOut.Point.Model.Dto
{
    public class AdPointDto
    {
        public string Location { get; set; }

        public TimeSpan StartWorkingTime { get; set; }

        public TimeSpan EndWorkingTime { get; set; }

        public int ScreenWidthCm { get; set; }

        public int ScreenHeightCm { get; set; }

        public IEnumerable<PlanDto> Plans { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<DayOfWeek> AdPointsDaysOff { get; set; }

        public IEnumerable<TariffDto> Tariffs { get; set; }
    }
}
