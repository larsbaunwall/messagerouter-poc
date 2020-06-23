using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Messaging.Support
{
    public class Envelope<T>
    {
        public string SourceBC { get; set; }
        public long Sequence { get; set; }
        public T Payload { get; set; }

        public Envelope(string sourceBC, T payload, long sequence)
        {
            SourceBC = sourceBC;
            Payload = payload;
            Sequence = sequence;
        }

        public Envelope()
        {
        }
    }
}