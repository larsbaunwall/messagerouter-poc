using System.Threading.Tasks;

namespace Messaging.Support
{
    public interface IEventPublisher
    {
        Task Publish<T>(string publishedLanguageEntity, T message, long sequence);

        Task Publish<T>(string exchange, string routingKey, Envelope<T> message);
    }
}