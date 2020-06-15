using System;
using EventConsumer.EventHandlers;
using EventConsumer.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventConsumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.RegisterEasyNetQ("host=localhost:32771;username=guest;password=guest;product=EventConsumer1");

                    services.AddScoped<IEventDispatcher, EventDispatcher>();
                    services.AddScoped<IEventHandler<SomethingHappendedEvent>, SomethingHappenedEventHandler>();
                    
                    services.AddHostedService<EventConsumerWorker>();
                });
    }
}