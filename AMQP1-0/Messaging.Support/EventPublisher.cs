using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Apache.NMS;

namespace Messaging.Support
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ISession _session;
        private readonly IMessageProducer _producer;
        private readonly string _boundedContextName;

        public EventPublisher(ISession session, IMessageProducer producer, string boundedContextName)
        {
            _session = session;
            _producer = producer;
            _boundedContextName = boundedContextName;
        }

        public async Task Publish<T>(string publishedLanguageEntity, T message, long sequence)
        {
            var envelope = new Envelope<T>(_boundedContextName, message, sequence);
            var msgBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(envelope);
            
            var topic = _session.GetTopic($"topic://{NamingConventions.Topic(_boundedContextName, publishedLanguageEntity)}");

            //var msg = _producer.CreateBytesMessage(msgBytes);
            var msg = _producer.CreateTextMessage(System.Text.Json.JsonSerializer.Serialize(envelope));
            
            _producer.Send(topic, msg, MsgDeliveryMode.Persistent, MsgPriority.Normal, TimeSpan.FromMinutes(5));
        }
    }
}