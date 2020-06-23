using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace DeferralService.Services
{
    public class DeferrerWorker : IHostedService
    {
        private readonly IDeferralRepository _repo;
        private Timer _timer;

        public DeferrerWorker(IDeferralRepository repo)
        {
            _repo = repo;
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
                _repo.Dequeue(id);
                Console.WriteLine($"Published message {id}");
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