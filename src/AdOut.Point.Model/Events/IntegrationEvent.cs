using System;

namespace AdOut.Point.Model.Events
{
    public abstract class IntegrationEvent
    {
        public Guid EventId { get; }

        public DateTime CreatedDateUtc { get; }

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            CreatedDateUtc = DateTime.UtcNow;
        }
    }
}
