﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jeppes.GatewayApi.Helpers
{
    public static class Deserializer
    {
        public static UsageDetails DeserializeUsageDetails(string json)
        {
            return JsonSerializer.Deserialize<UsageDetails>(json, JsonSerializerOptionsFactory.CreateOptions());
        }

        public static MessageStatus DeserializeMessageStatus(string json)
        {
            return JsonSerializer.Deserialize<MessageStatus>(json, JsonSerializerOptionsFactory.CreateOptions());
        }

        public static Message DeserializeMessage(string json)
        {
            return JsonSerializer.Deserialize<Message>(json, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
