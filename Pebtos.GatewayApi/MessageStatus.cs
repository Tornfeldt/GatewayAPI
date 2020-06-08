using Pebtos.GatewayApi.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi
{
    public class MessageStatus
    {
        /// <summary>
        /// The ID of the SMS/MMS this notification concerns.
        /// </summary>
        [JsonPropertyName("id")]
        public long MessageId { get; set; }

        /// <summary>
        /// Phone number of the recipient.
        /// </summary>
        [JsonPropertyName("msisdn")]
        [JsonConverter(typeof(PhoneNumberJsonConverter))]
        public PhoneNumber Recipient { get; set; }

        /// <summary>
        /// Timestamp for when the message was delivered.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeToUnixTimestampJsonConverter))]
        public DateTime? SendTimestamp { get; set; }

        /// <summary>
        /// Message status/delivery status.
        /// </summary>
        [JsonPropertyName("status")]
        public MessageDeliveryStatus Status { get; set; }

        /// <summary>
        /// Error decription, if available.
        /// </summary>
        [JsonPropertyName("error")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Numeric error code, in hex, see https://gatewayapi.com/docs/errors.html#smserror, if available.
        /// </summary>
        [JsonPropertyName("code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// The reference you set when sending the message.
        /// </summary>
        [JsonPropertyName("userref")]
        public string UserReference { get; set; }
    }
}
