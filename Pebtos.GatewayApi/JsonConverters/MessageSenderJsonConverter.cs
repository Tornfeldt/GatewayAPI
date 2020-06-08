using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi.JsonConverters
{
    internal class MessageSenderJsonConverter : JsonConverter<MessageSender>
    {
        public override bool CanConvert(Type type)
        {
            return typeof(MessageSender).IsAssignableFrom(type);
        }

        public override MessageSender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var phoneNumberString = reader.GetString();
            var sender = new StringMessageSender(phoneNumberString);
            return sender;
        }

        public override void Write(Utf8JsonWriter writer, MessageSender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.GenerateSenderString());
        }
    }
}
