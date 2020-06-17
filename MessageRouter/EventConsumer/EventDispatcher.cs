using System;
using System.Threading.Tasks;
using EasyNetQ;
using EventConsumer.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace EventConsumer
{
    public interface IEventDispatcher
    {
        Task HandleEvent<TEvent>(TEvent @event);
    }
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _services;

        public EventDispatcher(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task HandleEvent<TEvent>(TEvent @event)
        {
            var eventHandler = _services.GetService<IEventHandler<TEvent>>();
            await eventHandler.Handle(@event);
        }
    }
}