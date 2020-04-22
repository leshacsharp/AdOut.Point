using RabbitMQ.Client;

namespace AdOut.Point.Model.Interfaces.Infrastructure
{
    public interface IConnectionManager
    {
        IModel GetConsumerChannel();
        IModel GetPublisherChannel();
        void ReturnPublisherChannel(IModel channel);
    }
}
