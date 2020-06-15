using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi.Core
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum MessageDeliveryStatus
    {
        /// <summary>
        /// Messages start here, but you should not encounter this state.
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        Unknown,

        /// <summary>
        /// Used for messages where you set a sendtime in the future.
        /// </summary>
        [EnumMember(Value = "SCHEDULED")]
        Scheduled,

        /// <summary>
        /// The message is held in our internal queue and awaits delivery to the mobile network.
        /// </summary>
        [EnumMember(Value = "BUFFERED")]
        Buffered,

        /// <summary>
        /// Message has been sent to mobile network, and is on it’s way to it’s final destination.
        /// </summary>
        [EnumMember(Value = "ENROUTE")]
        Enroute,

        /// <summary>
        /// The end user’s mobile device has confirmed the delivery, and if message is charged the charge was successful.
        /// </summary>
        [EnumMember(Value = "DELIVERED")]
        Delivered,

        /// <summary>
        /// Message has exceeded it’s validity period without getting a delivery confirmation. No further delivery attempts.
        /// </summary>
        [EnumMember(Value = "EXPIRED")]
        Expired,

        /// <summary>
        /// Message was canceled.
        /// </summary>
        [EnumMember(Value = "DELETED")]
        Deleted,

        /// <summary>
        /// Message is permanently undeliverable. Most likely an invalid MSISDN (phone number).
        /// </summary>
        [EnumMember(Value = "UNDELIVERABLE")]
        Undeliverable,

        /// <summary>
        /// The mobile network has accepted the message on the end users behalf.
        /// </summary>
        [EnumMember(Value = "ACCEPTED")]
        Accepted,

        /// <summary>
        /// The mobile network has rejected the message. If this message was charged, the charge has failed.
        /// </summary>
        [EnumMember(Value = "REJECTED")]
        Rejected,

        /// <summary>
        /// The message was accepted, but was deliberately ignored due to network-specific rules.
        /// </summary>
        [EnumMember(Value = "SKIPPED")]
        Skipped
    }
}
