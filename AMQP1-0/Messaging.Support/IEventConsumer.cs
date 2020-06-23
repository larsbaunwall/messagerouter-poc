using System;
using System.Threading.Tasks;
using Amqp;

namespace Messaging.Support
{
    public interface IEventConsumer
    {
        Task<IDisposable> AddSubscription<TEvent>(string sourceBoundedContextName, string publishedLanguageEntity)
            where TEvent: class;
    }
}