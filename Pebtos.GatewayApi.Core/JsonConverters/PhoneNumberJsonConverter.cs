using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi.Core.JsonConverters
{
    public class PhoneNumberJsonConverter : JsonConverter<PhoneNumber>
    {
        public override PhoneNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var phoneNumber = reader.GetInt64();
            return PhoneNumber.Create(phoneNumber);
        }

        public override void Write(Utf8JsonWriter writer, PhoneNumber value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ConvertToMSISDN());
        }
    }
}
