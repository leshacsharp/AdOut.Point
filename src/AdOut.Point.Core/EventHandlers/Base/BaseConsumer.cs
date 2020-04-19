using AdOut.Point.Model.Events;
using AdOut.Point.Model.Exceptions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

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
                //todo: make constant for exceptionMessage
                var exceptionMessage = $"{this.GetType().Name} received message of the wrong type({typeof(TEvent).Name}) from exchange={exchange} and routingKey={routingKey}";
                throw new HandlerArgumentException(exceptionMessage, ex);
            }
        }

        protected abstract Task HandleAsync(TEvent deliveredEvent);
    }
}
