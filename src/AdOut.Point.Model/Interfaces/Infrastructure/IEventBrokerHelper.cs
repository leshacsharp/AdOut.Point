using System;

namespace AdOut.Point.Model.Interfaces.Infrastructure
{
    public interface IEventBrokerHelper
    {
        string GetQueueName(Type eventType);
        string GetExchangeName(Type eventType);
    }
}
