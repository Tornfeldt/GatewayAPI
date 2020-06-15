using Pebtos.GatewayApi.Core.JsonConverters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi.Core
{
    public class Message
    {
        /// <summary>
        /// Default is <see cref="MessageClass.Standard"/>.
        /// If specified it must be the same for all messages in the request.
        /// </summary>
        [JsonPropertyName("class")]
        public MessageClass Class { get; set; } = MessageClass.Standard;

        /// <summary>
        /// The content of the SMS.
        /// Required unless payload is specified.
        /// </summary>
        [JsonPropertyName("message")]
        public string MessageContent { get; set; }

        /// <summary>
        /// Up to 11 alphanumeric characters, or 15 digits, that will be shown as the sender of the SMS.
        /// This property is required.
        /// </summary>
        [JsonPropertyName("sender")]
        [JsonConverter(typeof(MessageSenderJsonConverter))]
        public MessageSender Sender { get; set; }

        /// <summary>
        /// Timestamp to schedule message sending at certain time in UTC.
        /// This property must be set to null if the message should be send now.
        /// </summary>
        [JsonPropertyName("sendtime")]
        [JsonConverter(typeof(DateTimeToUnixTimestampJsonConverter))]
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// A transparent string reference, you may set to keep track of the message in your own systems.
        /// Returned to you when you receive a delivery status notification.
        /// </summary>
        [JsonPropertyName("userref")]
        public string UserReference { get; set; }

        /// <summary>
        /// Urgent and Very Urgent normally require the use of premium message class.
        /// Default is <see cref="Priority.Normal"/>.
        /// </summary>
        [JsonPropertyName("priority")]
        public Priority Priority { get; set; } = Priority.Normal;

        /// <summary>
        /// If message is not delivered within this timespan, it will expire and you will get a notification.
        /// The minimum value is 60 seconds. Every value under 60 seconds will be set to 60 seconds.
        /// Property is not required.
        /// </summary>
        [JsonPropertyName("validity_period")]
        [JsonConverter(typeof(TimeSpanToSecondsJsonConverter))]
        public TimeSpan? ValidityPeriod { get; set; }

        /// <summary>
        /// Encoding to use when sending the message.
        /// Defaults to GSM 03.38. Use UCS2 to send a unicode message.
        /// </summary>
        [JsonPropertyName("encoding")]
        public SmsEncoding Encoding { get; set; } = SmsEncoding.GSM_03_38;

        /// <summary>
        /// Use <see cref="Destination.Display"/> to do “flash sms”, a message displayed on screen immediately but not saved in the normal message inbox on the mobile device.
        /// Property is not required.
        /// </summary>
        [JsonPropertyName("destaddr")]
        public Destination? Destination { get; set; }

        /// <summary>
        /// If you are sending a binary SMS, ie. a SMS you have encoded yourself or with speciel content for feature phones (non-smartphones), then you may specify a payload, encoded as Base64.
        /// If specified, <see cref="MessageContent"/> must not be set and tags are unavailable.
        /// </summary>
        [JsonPropertyName("payload")]
        public string Payload { get; set; }

        /// <summary>
        /// UDH (user defined header) to enable additional functionality for binary SMS, encoded as Base64.
        /// </summary>
        [JsonPropertyName("udh")]
        public string UDH { get; set; }

        /// <summary>
        /// If specified then status notifications are send to this URL. Otherwise, use the default webhook.
        /// </summary>
        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// A label added to each sent message, can be used to uniquely identify a customer or company that you sent the message on behalf of, to help with invoicing your customers.
        /// If specied it must be the same for all messages in the request.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// A number between 1 and 255 used to limit the number of smses a single message will send.
        /// Can be used if you send messages from systems that generates messages that you can’t control. This way you can ensure that you do not send very long SMSes.
        /// You will not be charged for more than the amount specified here.
        /// Can’t be used with tags or binary SMSes.
        /// </summary>
        [JsonPropertyName("max_parts")]
        public byte? MaximumNumberOfParts { get; set; }

        /// <summary>
        /// The number of recipients in a single message is limited to 10.000.
        /// This property is required.
        /// </summary>
        [JsonPropertyName("recipients")]
        public IReadOnlyCollection<Recipient> Recipients { get; set; }
    }
}
