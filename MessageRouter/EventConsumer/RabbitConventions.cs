using EasyNetQ;

namespace EventConsumer
{
    public class RabbitConventions : Conventions
    {
        public RabbitConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
        {
            QueueNamingConvention = (type, subId) => $"queue-{type.Name}-{subId}";
            ExchangeNamingConvention = type => $"exchange-{type.Name}";
        }
    }
}