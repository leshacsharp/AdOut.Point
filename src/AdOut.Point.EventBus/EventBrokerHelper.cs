﻿using AdOut.Point.Model.Interfaces.Infrastructure;
using System;
using System.Linq;

namespace AdOut.Point.EventBus
{
    public class EventBrokerHelper : IEventBrokerHelper
    {
        public string GetQueueName(Type eventType)
        {
            var eventName = eventType.Name;
            return FromCamelCaseToSnake(eventName) + "-queue";
        }

        public string GetExchangeName(Type eventType)
        {
            var eventName = eventType.Name;
            return FromCamelCaseToSnake(eventName) + "-exchange";
        }   

        private string FromCamelCaseToSnake(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
