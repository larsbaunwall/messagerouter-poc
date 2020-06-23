using System;
using System.Text;
using System.Threading.Tasks;
using Amqp.Framing;
using Apache.NMS;
using Apache.NMS.AMQP;

namespace Messaging.Support
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ISession _session;
        private readonly IConnection _conn;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly string _boundedContextName;
        private readonly string _subscriberId;

        public EventConsumer(ISession session, IConnection conn, IEventDispatcher eventDispatcher, string boundedContextName, string subscriberId)
        {
            _session = session;
            _conn = conn;
            _eventDispatcher = eventDispatcher;
            _boundedContextName = boundedContextName;
            _subscriberId = subscriberId;
        }
        
        public async Task<IDisposable> AddSubscription<TEvent>(string sourceBoundedContextName, string publishedLanguageEntity)
            where TEvent: class
        {
            var topic = _session.GetTopic($"topic://{NamingConventions.Topic(sourceBoundedContextName, publishedLanguageEntity)}");
            var consumer = _session.CreateConsumer(topic);
            consumer.Listener += async msg => await OnMessage<TEvent>(msg);
            if(!_conn.IsStarted)
                _conn.Start();
            return consumer;
        }

        private async Task OnMessage<TEvent>(IMessage message)
        {
            var msg = (ITextMessage) message;
            var envelope = System.Text.Json.JsonSerializer.Deserialize<Envelope<TEvent>>(msg.Text);
            
            var props = new MessageProperties();
            await _eventDispatcher.HandleEvent(envelope, props);
        }
    }
}