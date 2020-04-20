using AdOut.Point.Model.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using static AdOut.Point.Model.Constants;

namespace AdOut.Point.Core.EventHandlers.Base
{
    public abstract class BaseConsumer<TEvent> : AsyncDefaultBasicConsumer where TEvent : IntegrationEvent
    {
        public override Task HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            var jsonBody = Encoding.UTF8.GetString(body);
            try
            {
                var deliveredEvent = JsonConvert.DeserializeObject<TEvent>(jsonBody, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                return HandleAsync(deliveredEvent);
            }
            catch (JsonSerializationException ex)
            {
                var exceptionMessage = string.Format(ValidationMessages.EventHandlerWrongMessage_T, this.GetType().Name, exchange, routingKey);
                throw new ArgumentException(exceptionMessage, ex);
            }
        }

        protected abstract Task HandleAsync(TEvent deliveredEvent);
    }
}
