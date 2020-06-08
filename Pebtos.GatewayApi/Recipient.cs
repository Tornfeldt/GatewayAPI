using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi
{
    public class Recipient
    {
        /// <summary>
        /// Mobile phone number of the recipient.
        /// Duplicates are not allowed in the same message.
        /// This property is required.
        /// </summary>
        [JsonPropertyName("msisdn")]
        public PhoneNumber PhoneNumber { get; set; }
    }
}
