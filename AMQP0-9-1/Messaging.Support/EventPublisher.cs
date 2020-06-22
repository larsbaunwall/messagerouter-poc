using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace Messaging.Support
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IAdvancedBus _messageBus;
        private readonly string _boundedContextName;

        public EventPublisher(IAdvancedBus messageBus, string boundedContextName)
        {
            _messageBus = messageBus;
            _boundedContextName = boundedContextName;
        }

        public async Task Publish<T>(string publishedLanguageEntity, T message, long sequence)
        {
            var exchange = await _messageBus.ExchangeDeclareAsync(NamingConventions.ExchangeNamingConvention(_boundedContextName, publishedLanguageEntity),
                ExchangeType.Topic);
            
            var envelope = new Envelope<T>(_boundedContextName, message, sequence);
            await _messageBus.PublishAsync(exchange, "", false, new Message<Envelope<T>>(envelope, new EasyNetQ.MessageProperties {ContentType = "text/json"}));
        }
    }
}