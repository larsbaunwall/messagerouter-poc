using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace Messaging.Support
{
    public class EventConsumer : IEventConsumer
    {
        private readonly IAdvancedBus _messageBus;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly string _boundedContextName;
        private readonly string _subscriberId;

        public EventConsumer(IAdvancedBus messageBus, IEventDispatcher eventDispatcher, string boundedContextName, string subscriberId)
        {
            _messageBus = messageBus;
            _eventDispatcher = eventDispatcher;
            _boundedContextName = boundedContextName;
            _subscriberId = subscriberId;
        }
        
        public async Task<IDisposable> AddSubscription<TEvent>(string sourceBoundedContextName, string publishedLanguageEntity)
            where TEvent: class
        {
            var queue = await _messageBus.QueueDeclareAsync(NamingConventions.QueueNamingConvention(_boundedContextName, sourceBoundedContextName, publishedLanguageEntity, _subscriberId));
            await _messageBus.BindAsync(new Exchange(NamingConventions.ExchangeNamingConvention(sourceBoundedContextName, publishedLanguageEntity)), queue, "");

            return _messageBus.Consume(queue, async (bytes, properties, info) =>
            {
                var msg = Encoding.UTF8.GetString(bytes);
                var envelope = System.Text.Json.JsonSerializer.Deserialize<Envelope<TEvent>>(msg);
                
                var props = new MessageProperties();
                properties.CopyTo(props);
                await _eventDispatcher.HandleEvent(envelope, props);
            });
        }
    }
}