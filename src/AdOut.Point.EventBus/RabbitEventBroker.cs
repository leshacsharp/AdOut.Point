using AdOut.Point.Model.Events;
using AdOut.Point.Model.Interfaces.Infrastructure;
using AdOut.Point.Model.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdOut.Point.EventBus
{
    public class RabbitEventBroker : IEventBroker, IDisposable
    {
        private readonly IEventBrokerHelper _eventBrokerHelper;
        private readonly IConnection _connection;

        public RabbitEventBroker(
            IEventBrokerHelper eventBrokerHelper,
            IOptions<RabbitConnectionSettings> connectionOptions)
        {
            _eventBrokerHelper = eventBrokerHelper;

            var connectionFactory = new ConnectionFactory()
            {
                UserName = connectionOptions.Value.UserName,
                Password = connectionOptions.Value.Password,
                VirtualHost = connectionOptions.Value.VirtualHost,
                HostName = connectionOptions.Value.HostName
            };

            _connection = connectionFactory.CreateConnection();
        }

        public async Task PublishAsync(IEvent customEvent)
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, customEvent, customEvent.GetType());

            stream.Position = 0;
            var eventBody = new byte[stream.Length];
            await stream.ReadAsync(eventBody, 0, eventBody.Length);

            var exchange = _eventBrokerHelper.GetExchangeName(customEvent.GetType());
            var queue = _eventBrokerHelper.GetQueueName(customEvent.GetType());

            var channel = _connection.CreateModel();
            channel.BasicPublish(exchange, queue, null, eventBody);

            channel.Close();
        }

        public void Subscribe<TEvent>(IBasicConsumer eventHandler) where TEvent : IEvent
        {
            var queue = _eventBrokerHelper.GetQueueName(typeof(TEvent));

            var channel = _connection.CreateModel();
            channel.BasicConsume(queue, true, eventHandler);
            
            channel.Close();
        }

        public void Configure()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var eventTypes = assembly.GetTypes().Where(t => t.GetInterface(typeof(IEvent).Name) != null);

            var channel = _connection.CreateModel();
            foreach (var eventType in eventTypes)
            {
                var exchange = _eventBrokerHelper.GetExchangeName(eventType);
                var queue = _eventBrokerHelper.GetQueueName(eventType);

                channel.ExchangeDeclare(exchange, ExchangeType.Direct);
                channel.QueueDeclare(queue, true, false, true, null);
                channel.QueueBind(queue, exchange, queue, null);
            }

            channel.Close();
        }

        public void Dispose()
        {
            _connection.Close();
        }  
    }
}
