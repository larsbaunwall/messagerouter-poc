using System.Threading.Tasks;
using EasyNetQ;
using Messaging.Support;

namespace EventConsumer.EventHandlers
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(Envelope<TEvent> @event, MessageProperties props = null);
    }
}