using System;

namespace DeferralService.Models
{
    public class DeferMessageCommand
    {
        public string Message { get; set; }
        public string RecipientQueue { get; set; }
        public DateTimeOffset Until { get; set; }
    }
}