using System;
using System.Text.Json;
using EasyNetQ;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Messaging.Support
{
    public class NativeSerializer : ISerializer
    {
        public byte[] MessageToBytes(Type messageType, object message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message, messageType, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public object BytesToMessage(Type messageType, byte[] bytes)
        {
            return JsonSerializer.Deserialize(bytes, messageType);
        }
    }
}