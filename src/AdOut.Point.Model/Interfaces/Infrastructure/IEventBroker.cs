using AdOut.Point.Model.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace AdOut.Point.Model.Interfaces.Infrastructure
{
    public interface IEventBroker
    {
        void Publish(IntegrationEvent integrationEvent);
        void Subscribe(Type eventType, IBasicConsumer eventHandler);
        void Configure();
    }
}
