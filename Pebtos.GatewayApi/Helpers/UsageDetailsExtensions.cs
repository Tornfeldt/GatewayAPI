using Pebtos.GatewayApi.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pebtos.GatewayApi.Helpers
{
    public static class UsageDetailsExtensions
    {
        public static string ToJson(this UsageDetails usageDetails)
        {
            return JsonSerializer.Serialize(usageDetails, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
