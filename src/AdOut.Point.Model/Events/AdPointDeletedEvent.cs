namespace AdOut.Point.Model.Events
{
    public class AdPointDeletedEvent : IntegrationEvent
    {
        public int AdPointId { get; set; }
    }
}
