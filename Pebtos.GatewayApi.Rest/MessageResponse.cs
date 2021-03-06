﻿using System;
using System.Collections.Generic;
using System.Text;
using Pebtos.GatewayApi.Core;

namespace Pebtos.GatewayApi.Rest
{
    public class MessageResponse
    {
        internal MessageResponse(long messageId, Message sentMessage)
        {
            MessageId = messageId;
            SentMessage = sentMessage;
        }

        /// <summary>
        /// Message ID generated by the API.
        /// This can be used to retrive message status later.
        /// </summary>
        public long MessageId { get; }

        /// <summary>
        /// The message which was sent to the API.
        /// </summary>
        public Message SentMessage { get; }
    }
}
