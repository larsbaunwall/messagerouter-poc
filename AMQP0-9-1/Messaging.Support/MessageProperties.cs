using System;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;

namespace Messaging.Support
{
    public class MessageProperties : IMessageProperties
    {
        public ushort ProtocolClassId { get; }
        public string ProtocolClassName { get; }
        public void ClearAppId() => AppId = string.Empty;

        public void ClearClusterId() => ClusterId = string.Empty;
        public void ClearContentEncoding() => ContentEncoding = string.Empty;
        public void ClearContentType() => ContentType = string.Empty;
        public void ClearCorrelationId() => CorrelationId = string.Empty;
        public void ClearDeliveryMode() => DeliveryMode = new byte();
        public void ClearExpiration() => Expiration = string.Empty;
        public void ClearHeaders() => Headers.Clear();
        public void ClearMessageId() => MessageId = string.Empty;
        public void ClearPriority() => Priority = new byte();
        public void ClearReplyTo() => ReplyTo = string.Empty;
        public void ClearTimestamp() => Timestamp = new AmqpTimestamp(long.MinValue);
        public void ClearType() => Type = string.Empty;
        public void ClearUserId() => UserId = string.Empty;

        public bool IsAppIdPresent() => !string.IsNullOrEmpty(AppId);
        public bool IsClusterIdPresent() => !string.IsNullOrEmpty(ClusterId);
        public bool IsContentEncodingPresent() => !string.IsNullOrEmpty(ContentEncoding);
        public bool IsContentTypePresent() => !string.IsNullOrEmpty(ContentType);
        public bool IsCorrelationIdPresent() => !string.IsNullOrEmpty(CorrelationId);
        public bool IsDeliveryModePresent() => DeliveryMode != new byte();
        public bool IsExpirationPresent() => !string.IsNullOrEmpty(Expiration);
        public bool IsHeadersPresent() => Headers.Any();
        public bool IsMessageIdPresent() => !string.IsNullOrEmpty(MessageId);
        public bool IsPriorityPresent() => Priority != new byte();
        public bool IsReplyToPresent() => !string.IsNullOrEmpty(ReplyTo);
        public bool IsTimestampPresent() => Timestamp.UnixTime != long.MinValue;
        public bool IsTypePresent() => !string.IsNullOrEmpty(Type);
        public bool IsUserIdPresent() => !string.IsNullOrEmpty(UserId);

        public string AppId { get; set; }
        public string ClusterId { get; set; }
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public string CorrelationId { get; set; }
        public byte DeliveryMode { get; set; }
        public string Expiration { get; set; }
        public IDictionary<string, object> Headers { get; set; }
        public string MessageId { get; set; }
        public bool Persistent { get; set; }
        public byte Priority { get; set; }
        public string ReplyTo { get; set; }
        public PublicationAddress ReplyToAddress { get; set; }
        public AmqpTimestamp Timestamp { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
    }
}