using Pebtos.GatewayApi.Core;
using Pebtos.GatewayApi.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pebtos.GatewayApi.Rest.Helpers
{
    public static class Deserializer
    {
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
