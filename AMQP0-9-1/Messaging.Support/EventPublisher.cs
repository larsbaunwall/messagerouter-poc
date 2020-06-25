using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using Messaging.Support.Validation;

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
            var correlationId = Guid.NewGuid().ToString("N");
            var messageId = Guid.NewGuid().ToString("N");
            
            var envelope = new Envelope<T>(_boundedContextName, message, sequence, Guid.NewGuid().ToString("N"), correlationId)
            {
                Schema = typeof(T).GetAttribute<JsonSchemaAttribute>()?.SchemaUri 
            };
            await _messageBus.PublishAsync(exchange, "", false, new Message<Envelope<T>>(envelope, new EasyNetQ.MessageProperties
            {
                ContentType = "application/json", 
                CorrelationId = correlationId, 
                MessageId = messageId
            }));
        }
    }
}