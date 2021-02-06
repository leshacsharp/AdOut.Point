using AdOut.Extensions.Communication;

namespace AdOut.Point.Model.Events
{
    public class AdPointDeletedEvent : IntegrationEvent
    {
        public int Id { get; set; }
    }
}
