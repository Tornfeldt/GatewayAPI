using System;
using System.Collections.Generic;
using System.Text;

namespace Pebtos.GatewayApi.Webhook
{
    public class ForbiddenContext
    {
        internal ForbiddenContext(string authenticationFailMessage)
        {
            AuthenticationFailMessage = authenticationFailMessage;
        }

        /// <summary>
        /// Contains a message describing why authentication failed.
        /// </summary>
        public string AuthenticationFailMessage { get; internal set; }
    }
}
