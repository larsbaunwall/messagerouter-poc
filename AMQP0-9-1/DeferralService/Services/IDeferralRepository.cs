using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeferralService.Models;

namespace DeferralService.Services
{
    public interface IDeferralRepository
    {
        void Enqueue(Guid id, DeferMessageCommand cmd);
        void Dequeue(Guid id);
        IEnumerable<(Guid id, DeferMessageCommand message)> GetExpiredMessages(DateTimeOffset? since);
        
    }
}