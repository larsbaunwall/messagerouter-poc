using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using EventProducer.PublishedLanguage;
using Messaging.Support;
using Microsoft.Extensions.Hosting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EventProducer
{
    public class EventProducerWorker : IHostedService
    {
        private readonly IEventPublisher _eventPublisher;
        private Timer _timer;
        private int msgNo = 1;

        public EventProducerWorker(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Console.WriteLine($"Publishing message #{msgNo}");
            var msg = new Greeting {Gratulation = "Merry Christmas!", Sender = $"D. Vader"};

            _eventPublisher.Publish("Events.Greeting", msg, msgNo);
            msgNo++;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}