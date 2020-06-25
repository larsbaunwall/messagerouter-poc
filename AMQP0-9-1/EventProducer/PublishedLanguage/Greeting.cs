using Messaging.Support.Validation;

namespace EventProducer.PublishedLanguage
{
    [JsonSchema("https://raw.githubusercontent.com/larsbaunwall/messagerouter-sample/master/schemas/bounded-context/galactic-empire/events.greeting.json")]
    public class Greeting
    {
        public string Sender { get; set; }
        public string Gratulation { get; set; }
    }
}