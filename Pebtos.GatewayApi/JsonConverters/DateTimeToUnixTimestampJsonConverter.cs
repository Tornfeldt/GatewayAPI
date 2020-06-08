using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi.JsonConverters
{
    internal class DateTimeToUnixTimestampJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetInt64(out var unixSeconds))
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime;
            }
            else
            {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                var convertedDateTime = new DateTimeOffset(value.Value, TimeSpan.Zero);
                var unixTimestamp = convertedDateTime.ToUnixTimeSeconds();
                writer.WriteNumberValue(unixTimestamp);
            }
        }
    }
}
