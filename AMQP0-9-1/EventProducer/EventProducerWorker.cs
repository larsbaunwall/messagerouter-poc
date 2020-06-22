using System;
using System.Threading;
using System.Threading.Tasks;
using EventProducer.PublishedLanguage;
using Messaging.Support;
using Microsoft.Extensions.Hosting;

namespace EventProducer
{
    public class EventProducerWorker : IHostedService
    {
        private readonly IEventPublisher _eventPublisher;
        private Timer _timer;
        private int _msgNo = 1;
        private string[] _greetings = new[] {"Merry Christmas", "Happy Easter", "Have a nice vacation", "Happy Fathers Day", "Happy birthday to you!"};
        private string[] _senders = new[] {"Lord Vader", "S. Palpatine", "B. Fett", "Uncle Dooku"};

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
            var rnd = new Random();
            Console.WriteLine($"Publishing message #{_msgNo}");
            var msg = new Greeting
            {
                Gratulation = _greetings[rnd.Next(0, _greetings.Length)], 
                Sender = _senders[rnd.Next(0, _senders.Length)]
            };

            _eventPublisher.Publish("Events.Greeting", msg, _msgNo);
            _msgNo++;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}