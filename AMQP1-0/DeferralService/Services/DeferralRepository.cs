using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeferralService.Models;

namespace DeferralService.Services
{
    public class DeferralRepository : IDeferralRepository
    {
        public DeferralRepository()
        {
        }
        private static List<(Guid id, DeferMessageCommand message)> Repository { get; set; } = new List<(Guid id, DeferMessageCommand message)>();
        
        public void Enqueue(Guid id, DeferMessageCommand cmd)
        {
            Repository.Add((id, cmd));
        }

        public void Dequeue(Guid id)
        {
            Repository.RemoveAll(x => x.id == id);
        }

        public IEnumerable<(Guid id, DeferMessageCommand message)> GetExpiredMessages(DateTimeOffset? since)
        {
            return Repository.Where(x => x.message.Until <= (since ?? DateTimeOffset.Now));
        }
    }
}