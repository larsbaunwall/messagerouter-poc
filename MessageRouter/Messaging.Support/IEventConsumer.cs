using System;
using System.Threading.Tasks;

namespace Messaging.Support
{
    public interface IEventConsumer
    {
        Task<IDisposable> AddSubscription<TEvent>(string sourceBoundedContextName, string publishedLanguageEntity)
            where TEvent: class;
    }
}