using System.Threading.Tasks;
using EasyNetQ;

namespace Messaging.Support
{
    public interface IEventDispatcher
    {
        Task HandleEvent<TEvent>(Envelope<TEvent> @event, MessageProperties properties = null);
    }
}