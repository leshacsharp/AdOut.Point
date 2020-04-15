using AdOut.Point.Model.Events;
using AdOut.Point.Model.Interfaces.Infrastructure;
using AdOut.Point.Model.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

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

        public void Publish(IEvent customEvent)
        {    
            using (var channel = _connection.CreateModel())
            {
                var eventJson = JsonConvert.SerializeObject(customEvent, customEvent.GetType(), new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                var messageBody = Encoding.UTF8.GetBytes(eventJson);
                var exchange = _eventBrokerHelper.GetExchangeName(customEvent.GetType());

                channel.BasicPublish(exchange, null, null, messageBody);
            }
        }

        public void Subscribe<TEvent>(IBasicConsumer eventHandler) where TEvent : IEvent
        {         
            using (var channel = _connection.CreateModel())
            {
                var queue = _eventBrokerHelper.GetQueueName(typeof(TEvent));
                channel.BasicConsume(queue, true, eventHandler);
            }
        }

        public void Configure(IEnumerable<Type> eventTypes)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //var eventTypes = assembly.GetTypes().Where(t => t.GetInterface(typeof(IEvent).Name) != null);

            using (var channel = _connection.CreateModel())
            {
                foreach (var eventType in eventTypes)
                {
                    var exchange = _eventBrokerHelper.GetExchangeName(eventType);
                    var queue = _eventBrokerHelper.GetQueueName(eventType);

                    channel.ExchangeDeclare(exchange, ExchangeType.Fanout);
                    channel.QueueDeclare(queue, true, false, true, null);
                    channel.QueueBind(queue, exchange, null, null);
                }
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }  
    }
}
