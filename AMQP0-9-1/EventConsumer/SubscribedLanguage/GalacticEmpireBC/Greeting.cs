using Messaging.Support;
using Messaging.Support.Validation;

namespace EventConsumer.SubscribedLanguage.GalacticEmpireBC
{
    [JsonSchema("https://raw.githubusercontent.com/larsbaunwall/messagerouter-sample/master/schemas/bounded-context/galactic-empire/events.greeting.json")]
    public class Greeting
    {
        public string Sender { get; set; }
        public string Gratulation { get; set; }
    }
}