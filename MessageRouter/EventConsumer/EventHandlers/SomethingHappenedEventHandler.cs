using System;
using System.Threading.Tasks;
using EventConsumer.Messages;

namespace EventConsumer.EventHandlers
{
    public class SomethingHappenedEventHandler : IEventHandler<SomethingHappendedEvent>
    {
        public Task Handle(SomethingHappendedEvent @event)
        {
            Console.WriteLine($"Message received: {@event.Gratulation}");
            
            return Task.CompletedTask;
            
        }
    }
}