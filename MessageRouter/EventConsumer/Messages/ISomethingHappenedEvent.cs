namespace EventConsumer.Messages
{
    public interface ISomethingHappenedEvent
    {
        string Sender { get; set; }
        string Gratulation { get; set; }
    }
}