using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Jeppes.GatewayApi
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum Priority
    {
        [EnumMember(Value = "NORMAL")]
        Normal,

        [EnumMember(Value = "BULK")]
        Bulk,

        [EnumMember(Value = "URGENT")]
        Urgent,

        [EnumMember(Value = "VERY_URGENT")]
        VeryUrgent
    }
}
