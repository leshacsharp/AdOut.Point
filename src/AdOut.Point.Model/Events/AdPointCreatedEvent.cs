using System;

namespace AdOut.Point.Model.Events
{
    public class AdPointCreatedEvent : IntegrationEvent
    {
        public string Location { get; set; }

        public string IpAdress { get; set; }

        public TimeSpan StartWorkingTime { get; set; }

        public TimeSpan EndWorkingTime { get; set; }

        public int ScreenWidthCm { get; set; }

        public int ScreenHeightCm { get; set; }
    }
}
