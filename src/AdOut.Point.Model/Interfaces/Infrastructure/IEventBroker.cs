using AdOut.Point.Model.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace AdOut.Point.Model.Interfaces.Infrastructure
{
    public interface IEventBroker
    {
        void Publish(IEvent customEvent);
        void Subscribe<TEvent>(IBasicConsumer eventHandler) where TEvent : IEvent;
        void Configure(IEnumerable<Type> eventTypes);
    }
}
