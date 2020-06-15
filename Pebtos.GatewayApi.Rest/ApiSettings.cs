using System;
using System.Collections.Generic;
using System.Text;

namespace Pebtos.GatewayApi.Rest
{
    public class ApiSettings
    {
        /// <summary>
        /// API URL.
        /// Default is https://gatewayapi.com/rest
        /// Do not set this value unless you want to send the requests to another URL.
        /// </summary>
        public string Url { get; set; } = "https://gatewayapi.com/rest";

        /// <summary>
        /// The API token must be set.
        /// You can find your API token by logging in to the administration on https://gatewayapi.com
        /// </summary>
        public string ApiToken { get; set; }
    }
}
