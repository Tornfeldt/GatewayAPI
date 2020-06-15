using System;
using System.Collections.Generic;
using System.Text;

namespace Pebtos.GatewayApi.Rest
{
    public class MessagesSentResponse<T>
    {
        internal MessagesSentResponse(UsageDetails usageDetails, T messageResponseDetails)
        {
            UsageDetails = usageDetails;
            MessageResponseDetails = messageResponseDetails;
        }

        /// <summary>
        /// Details about what the cost is for sending the messages in the request.
        /// </summary>
        public UsageDetails UsageDetails { get; }

        /// <summary>
        /// Details about the message response.
        /// </summary>
        public T MessageResponseDetails { get; }
    }
}
