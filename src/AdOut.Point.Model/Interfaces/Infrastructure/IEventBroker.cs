using AdOut.Point.Model.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Infrastructure
{
    public interface IEventBroker
    {
        Task PublishAsync(IEvent customEvent);
        void Subscribe<TEvent>(IBasicConsumer eventHandler) where TEvent : IEvent;
        void Configure();
    }
}
