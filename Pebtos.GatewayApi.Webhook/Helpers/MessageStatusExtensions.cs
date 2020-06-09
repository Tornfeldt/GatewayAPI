using Pebtos.GatewayApi.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pebtos.GatewayApi.Webhook.Helpers
{
    public static class MessageStatusExtensions
    {
        public static string ToJson(this MessageStatus messageStatus)
        {
            return JsonSerializer.Serialize(messageStatus, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
