using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Hosting;

namespace DeferralService.Services
{
    public class DeferrerWorker : IHostedService
    {
        private readonly IDeferralRepository _repo;
        private readonly IBus _messageBus;
        private Timer _timer;

        public DeferrerWorker(IDeferralRepository repo, IBus messageBus)
        {
            _repo = repo;
            _messageBus = messageBus;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {            
            var messages = _repo.GetExpiredMessages(DateTimeOffset.Now);

            foreach (var (id, message) in messages)
            {
                _messageBus.Publish(message, message.RecipientQueue);
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