using Jeppes.GatewayApi.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jeppes.GatewayApi.Helpers
{
    public static class MessageExtensions
    {
        public static string ToJson(this Message message)
        {
            return JsonSerializer.Serialize(message, JsonSerializerOptionsFactory.CreateOptions());
        }

        public static string ToJson(this IEnumerable<Message> messages)
        {
            return JsonSerializer.Serialize(messages, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
