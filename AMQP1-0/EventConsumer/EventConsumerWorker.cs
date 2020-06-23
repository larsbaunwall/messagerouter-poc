using System.Threading;
using System.Threading.Tasks;
using Amqp;
using EventConsumer.SubscribedLanguage.GalacticEmpireBC;
using Messaging.Support;
using Microsoft.Extensions.Hosting;

namespace EventConsumer
{
    public class EventConsumerWorker : IHostedService 
    
    {
        private readonly IEventConsumer _eventConsumer;

        public EventConsumerWorker(IEventConsumer eventConsumer)
        {
            _eventConsumer = eventConsumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _eventConsumer.AddSubscription<Greeting>("GalacticEmpireBC", "Events.Greeting");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}