using System.Text.Json.Serialization;
using EasyNetQ;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Messaging.Support
{
    public class Envelope<T>
    {
        [JsonPropertyName("$schema")]
        public string Schema { get; set; }
        public string MessageId { get; set; }
        public string CorrelationId { get; set; }
        public string SourceBC { get; set; }
        public long Sequence { get; set; }
        public T Payload { get; set; }

        public Envelope(string sourceBC, T payload, long sequence, string messageId, string correlationId)
        {
            SourceBC = sourceBC;
            Payload = payload;
            Sequence = sequence;
            MessageId = messageId;
            CorrelationId = correlationId;
        }

        public Envelope(string messageId, string correlationId)
        {
            MessageId = messageId;
            CorrelationId = correlationId;
        }
    }
}