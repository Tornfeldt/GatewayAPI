using Pebtos.GatewayApi.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pebtos.GatewayApi.Core.Helpers
{
    public class Deserializer
    {
        public static MessageStatus DeserializeMessageStatus(string json)
        {
            return JsonSerializer.Deserialize<MessageStatus>(json, JsonSerializerOptionsFactory.CreateOptions());
        }

        public static UsageDetails DeserializeUsageDetails(string json)
        {
            return JsonSerializer.Deserialize<UsageDetails>(json, JsonSerializerOptionsFactory.CreateOptions());
        }

        public static Message DeserializeMessage(string json)
        {
            return JsonSerializer.Deserialize<Message>(json, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
