using System;
using System.Threading.Tasks;
using EventConsumer.Messages;

namespace EventConsumer.EventHandlers
{
    public class SomethingHappenedEventHandler : IEventHandler<ISomethingHappenedEvent>
    {
        public Task Handle(ISomethingHappenedEvent @event)
        {
            Console.WriteLine($"Message received: {@event.Gratulation}, {@event.Sender}");
            
            return Task.CompletedTask;
            
        }
    }
}