using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pebtos.GatewayApi.Webhook
{
    public class WebhookOptions
    {
        internal WebhookOptions() { }

        /// <summary>
        /// The secret (or token) you specified in the GatewayAPI administration.
        /// </summary>
        public string JwtSecret { get; set; }

        /// <summary>
        /// Name of the header of the JWT token.
        /// </summary>
        public string HeaderName { get; } = "X-Gwapi-Signature";

        /// <summary>
        /// Invoked before the JWT token is validated.
        /// </summary>
        public Func<TokenValidatingContext, Task> OnTokenValidatingAsync { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Invoked after the token has been validated successfully.
        /// </summary>
        public Func<TokenValidatedContext, Task> OnTokenValidatedAsync { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Invoked if the JWT token is not valid and results in a Forbidden result.
        /// </summary>
        public Func<ForbiddenContext, Task> OnForbiddenAsync { get; set; } = context => Task.CompletedTask;
    }
}
