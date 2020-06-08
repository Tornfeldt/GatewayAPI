using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Destination
    {
        [EnumMember(Value = "DISPLAY")]
        Display,

        [EnumMember(Value = "MOBILE")]
        Mobile,

        [EnumMember(Value = "SIMCARD")]
        SimCard,

        [EnumMember(Value = "EXTUNIT")]
        ExtUnit
    }
}
