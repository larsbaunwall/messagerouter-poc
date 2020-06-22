using System.Threading.Tasks;
using Messaging.Support;

namespace EventConsumer.EventHandlers
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(Envelope<TEvent> @event, IMessageProperties props = null);
    }
}