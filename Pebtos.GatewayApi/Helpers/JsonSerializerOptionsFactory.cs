using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pebtos.GatewayApi.Helpers
{
    internal static class JsonSerializerOptionsFactory
    {
        internal static JsonSerializerOptions CreateOptions() => new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
    }
}
