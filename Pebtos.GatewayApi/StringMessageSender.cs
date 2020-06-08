using System;
using System.Collections.Generic;
using System.Text;

namespace Pebtos.GatewayApi
{
    public class StringMessageSender : MessageSender
    {
        private readonly string _sender;

        public StringMessageSender(string sender)
        {
            _sender = sender;
        }

        internal override string GenerateSenderString()
        {
            return _sender;
        }
    }
}
