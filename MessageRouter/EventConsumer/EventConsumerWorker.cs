using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EventConsumer.Messages;
using Microsoft.Extensions.Hosting;

namespace EventConsumer
{
    public class EventConsumerWorker : IHostedService 
    
    {
        private readonly string _subscriberId = Environment.MachineName;
        private readonly IBus _messageBus;
        private readonly IEventDispatcher _dispatcher;

        public EventConsumerWorker(IBus messageBus, IEventDispatcher dispatcher)
        {
            _messageBus = messageBus;
            _dispatcher = dispatcher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            AddSubscription<ISomethingHappenedEvent>();
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void AddSubscription<TEvent>()
            where TEvent: class
        {
            _messageBus.SubscribeAsync<TEvent>(_subscriberId, _dispatcher.HandleEvent);
        }
    }
}