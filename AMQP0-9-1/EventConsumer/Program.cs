using System;
using EasyNetQ;
using EventConsumer.EventHandlers;
using EventConsumer.SubscribedLanguage;
using EventConsumer.SubscribedLanguage.GalacticEmpireBC;
using Messaging.Support;
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
                    services.RegisterEasyNetQ("host=localhost:32779;username=guest;password=guest;product=EventConsumer1");

                    services.AddScoped<IEventDispatcher, EventDispatcher>();
                    services.AddScoped<IEventConsumer, Messaging.Support.EventConsumer>(provider =>
                        new Messaging.Support.EventConsumer(
                            provider.GetService<IAdvancedBus>(), 
                            provider.GetService<IEventDispatcher>(), 
                            "RebelAllianceBC",
                            $"EventConsumer1.{Environment.MachineName}"));
                    services.AddScoped<IEventHandler<Greeting>, SomethingHappenedEventHandler>();
                    
                    services.AddHostedService<EventConsumerWorker>();
                });
    }
}