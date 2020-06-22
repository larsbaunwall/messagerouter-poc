using System;
using EasyNetQ;
using Messaging.Support;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureMessaging(
            this IServiceCollection serviceCollection, 
            string brokerConnectionString, 
            string boundedContextName,
            string serviceName)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }
            
            serviceCollection.RegisterEasyNetQ(brokerConnectionString);
            
            serviceCollection.AddScoped<IEventConsumer, EventConsumer>(provider =>
                new EventConsumer(
                    provider.GetService<IAdvancedBus>(), 
                    provider.GetService<IEventDispatcher>(), 
                    boundedContextName,
                    $"{serviceName}.{Environment.MachineName}"));
            
            serviceCollection.AddScoped<IEventPublisher, EventPublisher>(provider =>
                new EventPublisher(provider.GetService<IAdvancedBus>(),
                    boundedContextName));
            
            return serviceCollection;
        }
    }
}