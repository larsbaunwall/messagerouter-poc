using System.Threading.Tasks;

namespace Messaging.Support
{
    public interface IEventPublisher
    {
        Task Publish<T>(string publishedLanguageEntity, T message, long sequence);
    }
}