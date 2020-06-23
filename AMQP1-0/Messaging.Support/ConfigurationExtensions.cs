using System;
using Apache.NMS;
using Apache.NMS.AMQP;
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
            
            var addr = new Uri(brokerConnectionString);

            serviceCollection.AddScoped<Apache.NMS.IConnectionFactory>(provider => new NmsConnectionFactory(addr));
            serviceCollection.AddSingleton<Apache.NMS.IConnection>(provider =>
                provider.GetService<IConnectionFactory>().CreateConnection());

            serviceCollection.AddSingleton<ISession>(provider =>
                provider.GetService<IConnection>().CreateSession(AcknowledgementMode.AutoAcknowledge));

            serviceCollection.AddSingleton<IMessageProducer>(provider =>
                provider.GetService<ISession>().CreateProducer());
            
            serviceCollection.AddScoped<IEventConsumer, EventConsumer>(provider =>
                new EventConsumer(
                    provider.GetService<ISession>(), 
                    provider.GetService<IConnection>(),
                    provider.GetService<IEventDispatcher>(), 
                    boundedContextName,
                    $"{serviceName}.{Environment.MachineName}"));
            
            serviceCollection.AddScoped<IEventPublisher, EventPublisher>(provider =>
                new EventPublisher(provider.GetService<ISession>(),
                    provider.GetService<IMessageProducer>(),
                    boundedContextName));
            
            return serviceCollection;
        }
    }
}