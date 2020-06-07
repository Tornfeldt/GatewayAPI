using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Jeppes.GatewayApi
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum SmsEncoding
    {
        [EnumMember(Value = "UTF8")]
        GSM_03_38,

        [EnumMember(Value = "UCS2")]
        USC2
    }
}
