﻿using AdOut.Point.Model.Interfaces.Infrastructure;
using AdOut.Point.Model.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace AdOut.Point.EventBroker
{
    public class RabbitConnectionManager : IConnectionManager, IDisposable
    {
        private readonly IConnection _connection;
        private readonly Queue<IModel> _sharedChannels;
        private readonly object _syncDequeue = new object();
        private readonly object _syncEnqueue = new object();
        private readonly int _maxChannelsInPool;

        public RabbitConnectionManager(IOptions<RabbitConfig> config)
        {
            var connectionFactory = new ConnectionFactory()
            {
                DispatchConsumersAsync = true,
                UserName = config.Value.UserName,
                Password = config.Value.Password,
                VirtualHost = config.Value.VirtualHost,
                HostName = config.Value.HostName
            };

            _connection = connectionFactory.CreateConnection();
            _sharedChannels = new Queue<IModel>();
            _maxChannelsInPool = config.Value.ChannelsPool;
        }

        public IModel GetConsumerChannel()
        {
            return _connection.CreateModel();
        }

        public IModel GetPublisherChannel()
        {
            lock (_syncDequeue)
            {
                if (_sharedChannels.Count > 0)
                {
                    return _sharedChannels.Dequeue();
                }
                else
                {
                    return _connection.CreateModel();
                }
            }
        }

        public void ReturnPublisherChannel(IModel channel)
        {
            lock (_syncEnqueue)
            {
                if (_sharedChannels.Count < _maxChannelsInPool)
                {
                    if (channel.IsOpen)
                    {
                        _sharedChannels.Enqueue(channel);
                    }
                    else
                    {
                        channel.Dispose();
                    }
                }
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
