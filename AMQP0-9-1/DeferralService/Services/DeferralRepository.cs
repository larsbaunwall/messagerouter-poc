using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DeferralService.Models;
using EasyNetQ;

namespace DeferralService.Services
{
    public class DeferralRepository : IDeferralRepository
    {
        private readonly IBus _messageBus;

        public DeferralRepository(IBus messageBus)
        {
            _messageBus = messageBus;
        }
        private static ConcurrentDictionary<Guid, DeferMessageCommand> Repository { get; set; } = new ConcurrentDictionary<Guid, DeferMessageCommand>();
        
        public void Enqueue(Guid id, DeferMessageCommand cmd)
        {
            Repository.Add(id, cmd);
        }

        public void Dequeue(Guid id)
        {
            Repository.Remove(id);
        }

        public IEnumerable<(Guid id, DeferMessageCommand message)> GetExpiredMessages(DateTimeOffset? since)
        {
            return Repository.Where(x => x.Value.Until <= (since ?? DateTimeOffset.Now))
                .Select(x => (x.Key, x.Value));
        }
    }
}