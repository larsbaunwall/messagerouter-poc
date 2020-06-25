using System;
using EasyNetQ;
using Messaging.Support;
using Messaging.Support.Validation;

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
            
            serviceCollection.RegisterEasyNetQ(brokerConnectionString, register =>
            {
                register.Register<ISerializer, NativeSerializer>();
            });
            
            serviceCollection.AddScoped<IEventConsumer, EventConsumer>(provider =>
                new EventConsumer(
                    provider.GetService<IAdvancedBus>(), 
                    provider.GetService<IEventDispatcher>(), 
                    provider.GetService<ISchemaValidator>(),
                    boundedContextName,
                    $"{serviceName}.{Environment.MachineName}"));
            
            serviceCollection.AddScoped<IEventPublisher, EventPublisher>(provider =>
                new EventPublisher(provider.GetService<IAdvancedBus>(),
                    boundedContextName));

            serviceCollection.AddSingleton<ISchemaValidator, JsonSchemaValidator>();
            
            return serviceCollection;
        }
    }
}