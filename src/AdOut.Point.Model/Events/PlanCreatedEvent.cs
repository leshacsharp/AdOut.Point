using System;
using System.Collections.Generic;
using System.Text;

namespace AdOut.Point.Model.Events
{
    //todo: need to add other information
    public class PlanCreatedEvent : IntegrationEvent
    {
        public string Title { get; set; }
    }
}
