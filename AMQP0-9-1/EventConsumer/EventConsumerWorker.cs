using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using EventConsumer.SubscribedLanguage;
using EventConsumer.SubscribedLanguage.GalacticEmpireBC;
using Messaging.Support;
using Microsoft.Extensions.Hosting;
using JsonSerializer = System.Text.Json.JsonSerializer;

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