using RabbitMQ.Client;

namespace AdOut.Point.Model.Interfaces.Infrastructure
{
    public interface IConnectionProvider
    {
        IConnection CreateConnection();
    }
}
