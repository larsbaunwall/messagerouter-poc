using System;
using System.Threading.Tasks;
using EasyNetQ;
using EventConsumer.EventHandlers;
using Messaging.Support;
using Microsoft.Extensions.DependencyInjection;

namespace EventConsumer
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _services;

        public EventDispatcher(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task HandleEvent<TEvent>(Envelope<TEvent> @event, MessageProperties properties = null)
        {
            var eventHandler = _services.GetService<IEventHandler<TEvent>>();
            await eventHandler.Handle(@event, properties);
        }
    }
}