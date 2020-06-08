using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pebtos.GatewayApi
{
    public class UsageDetails
    {
        /// <summary>
        /// Currency in which you are billed.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Total cost in <see cref="Currency"/> of sending the request.
        /// </summary>
        [JsonPropertyName("total_cost")]
        public decimal TotalCost { get; set; }
    }
}
