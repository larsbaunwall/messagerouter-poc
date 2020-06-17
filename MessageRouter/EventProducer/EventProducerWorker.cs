using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EventProducer.Messages;
using Microsoft.Extensions.Hosting;

namespace EventProducer
{
    public class EventProducerWorker : IHostedService
    {
        private readonly IBus _messageBus;
        private Timer _timer;

        public EventProducerWorker(IBus messageBus)
        {
            _messageBus = messageBus;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            Console.WriteLine("Publishing message");
            _messageBus.Publish(new SomethingHappenedEvent {Gratulation = "Merry Christmas!", Sender = "Galactic Empire"});
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}