using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jeppes.GatewayApi.JsonConverters
{
    public class TimeSpanToSecondsJsonConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetInt64(out var seconds))
            {
                return TimeSpan.FromSeconds(seconds);
            }
            else
            {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteNumberValue((long)value.Value.TotalSeconds);
            }
        }
    }
}
