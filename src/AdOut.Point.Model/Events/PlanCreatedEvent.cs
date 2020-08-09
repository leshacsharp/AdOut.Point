namespace AdOut.Point.Model.Events
{
    //todo: need to add other information
    public class PlanCreatedEvent : IntegrationEvent
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
