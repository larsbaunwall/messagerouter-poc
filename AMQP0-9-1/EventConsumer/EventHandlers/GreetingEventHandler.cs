using System;
using System.Threading.Tasks;
using EasyNetQ;
using EventConsumer.SubscribedLanguage.GalacticEmpireBC;
using Messaging.Support;

namespace EventConsumer.EventHandlers
{
    public class SomethingHappenedEventHandler : IEventHandler<Greeting>
    {
        public Task Handle(Envelope<Greeting> @event, MessageProperties props = null)
        {
            Console.WriteLine($"Message received (#{@event.Sequence}): {@event.Payload.Gratulation}, {@event.Payload.Sender} - source BC: {@event.SourceBC}");
            
            return Task.CompletedTask;
            
        }
    }
}