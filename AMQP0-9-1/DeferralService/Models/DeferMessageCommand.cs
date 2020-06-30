using System;
using Messaging.Support;
using RabbitMQ.Client;

namespace DeferralService.Models
{
    public class DeferMessageCommand
    {
        public Envelope<object> OriginalMessage { get; set; }
        public IBasicProperties OriginalMessageProperties { get; set; }
        public string RecipientExchange { get; set; }
        public string RoutingKey { get; set; }
        public DateTimeOffset Until { get; set; }
    }
}