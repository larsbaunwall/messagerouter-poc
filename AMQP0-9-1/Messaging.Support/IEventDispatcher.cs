using System.Threading.Tasks;
namespace Messaging.Support
{
    public interface IEventDispatcher
    {
        Task HandleEvent<TEvent>(Envelope<TEvent> @event, IMessageProperties properties = null);
    }
}