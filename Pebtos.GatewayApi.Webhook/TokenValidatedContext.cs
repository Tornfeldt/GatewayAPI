using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Pebtos.GatewayApi.Webhook
{
    public class TokenValidatedContext
    {
        internal TokenValidatedContext() { }

        public ClaimsPrincipal Principal { get; internal set; }
    }
}
