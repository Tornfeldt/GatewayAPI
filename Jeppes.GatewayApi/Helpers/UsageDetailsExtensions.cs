using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Jeppes.GatewayApi.Helpers
{
    public static class UsageDetailsExtensions
    {
        public static string ToJson(this UsageDetails usageDetails)
        {
            return JsonSerializer.Serialize(usageDetails, JsonSerializerOptionsFactory.CreateOptions());
        }
    }
}
