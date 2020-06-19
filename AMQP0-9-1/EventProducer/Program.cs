using System;
using EasyNetQ;
using Messaging.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventProducer
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
                    services.RegisterEasyNetQ("host=localhost:32779;username=guest;password=guest;product=EventProducer1");

                    services.AddScoped<IEventPublisher, EventPublisher>(provider =>
                        new EventPublisher(provider.GetService<IAdvancedBus>(), "GalacticEmpireBC"));
                    
                    services.AddHostedService<EventProducerWorker>();
                });
    }
}