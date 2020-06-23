﻿using EventConsumer.EventHandlers;
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
                    services.ConfigureMessaging(
                        "amqp://guest:guest@localhost:32772?nms.clientId=RebelAllianceBC.EventConsumer1",
                        "RebelAllianceBC",
                        "EventConsumer1");

                    services.AddScoped<IEventDispatcher, EventDispatcher>();
                    services.AddScoped<IEventHandler<Greeting>, SomethingHappenedEventHandler>();
                    
                    services.AddHostedService<EventConsumerWorker>();
                });
    }
}