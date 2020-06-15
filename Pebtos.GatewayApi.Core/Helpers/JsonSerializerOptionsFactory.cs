using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Pebtos.GatewayApi.Core.Helpers
{
    public static class JsonSerializerOptionsFactory
    {
        public static JsonSerializerOptions CreateOptions() => new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
    }
}
