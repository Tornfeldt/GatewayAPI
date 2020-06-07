using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jeppes.GatewayApi.Helpers
{
    public static class MessageStatusExtensions
    {
        public static string ToJson(this MessageStatus messageStatus)
        {
            return JsonSerializer.Serialize(messageStatus, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
