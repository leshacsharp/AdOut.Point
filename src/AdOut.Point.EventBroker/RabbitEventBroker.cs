﻿using AdOut.Point.Model.Events;
using AdOut.Point.Model.Interfaces.Infrastructure;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace AdOut.Point.EventBroker
{
    public class RabbitEventBroker : IEventBroker
    {
        private readonly IChannelManager _channelManager;
        private readonly IEventBrokerHelper _eventBrokerHelper;

        public RabbitEventBroker(
            IChannelManager channelManager,
            IEventBrokerHelper eventBrokerHelper)
        {
            _channelManager = channelManager;
            _eventBrokerHelper = eventBrokerHelper;
        }

        public void Publish(IntegrationEvent integrationEvent)
        {
            var eventJson = JsonConvert.SerializeObject(integrationEvent, new TypeInfoConverter(integrationEvent.GetType()));
            var messageBody = Encoding.UTF8.GetBytes(eventJson);

            var exchange = _eventBrokerHelper.GetExchangeName(integrationEvent.GetType());
            var routingKey = _eventBrokerHelper.GetQueueName(integrationEvent.GetType());

            var channel = _channelManager.GetPublisherChannel();
            channel.BasicPublish(exchange, routingKey, null, messageBody);

            _channelManager.ReturnPublisherChannel(channel);
        }

        public void Subscribe(Type eventType, IBasicConsumer eventHandler)
        {
            var queue = _eventBrokerHelper.GetQueueName(eventType);

            var channel = _channelManager.GetConsumerChannel();
            channel.BasicConsume(queue, true, eventHandler);
        }

        public void Configure()
        {
            var channel = _channelManager.GetPublisherChannel();

            var modelAssembly = typeof(Model.Constants).Assembly;
            var eventTypes = modelAssembly.GetTypes().Where(t => t.BaseType == typeof(IntegrationEvent));

            foreach (var eventType in eventTypes)
            {
                var exchange = _eventBrokerHelper.GetExchangeName(eventType);
                var queue = _eventBrokerHelper.GetQueueName(eventType);

                channel.ExchangeDeclare(exchange, ExchangeType.Fanout);
                channel.QueueDeclare(queue, true, false, false, null);
                channel.QueueBind(queue, exchange, queue, null);
            }

            _channelManager.ReturnPublisherChannel(channel);
        }
    }
}
