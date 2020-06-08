using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum MessageClass
    {
        [EnumMember(Value = "standard")]
        Standard,

        [EnumMember(Value = "premium")]
        Premium,

        /// <summary>
        /// The secret class can be used to blur the message content you send, used for very sensitive data.
        /// It is priced as premium and uses the same routes, which ensures end to end encryption of your messages.
        /// Access to the secret class will be very strictly controlled.
        /// </summary>
        [EnumMember(Value = "secret")]
        Secret
    }
}
