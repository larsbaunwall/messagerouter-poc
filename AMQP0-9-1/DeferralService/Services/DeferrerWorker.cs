using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Messaging.Support;
using Microsoft.Extensions.Hosting;

namespace DeferralService.Services
{
    public class DeferrerWorker : IHostedService
    {
        private readonly IDeferralRepository _repo;
        private readonly IEventPublisher _publisher;
        private Timer _timer;

        public DeferrerWorker(IDeferralRepository repo, IEventPublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {            
            var messages = _repo.GetExpiredMessages(DateTimeOffset.Now);

            Console.WriteLine($"Enqueing {messages.Count()} messages");
            
            foreach (var (id, message) in messages)
            {
                _publisher.Publish(message.RecipientExchange, message.RoutingKey, message.OriginalMessage);
                _repo.Dequeue(id);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}