using System.Threading.Tasks;

namespace EventConsumer.EventHandlers
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}