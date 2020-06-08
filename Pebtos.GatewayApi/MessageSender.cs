using System;
using System.Collections.Generic;
using System.Text;

namespace Pebtos.GatewayApi
{
    public abstract class MessageSender
    {
        internal abstract string GenerateSenderString();
    }
}
