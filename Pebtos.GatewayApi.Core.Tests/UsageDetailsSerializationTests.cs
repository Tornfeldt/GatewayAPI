using Pebtos.GatewayApi.Core.Helpers;
using System;
using System.Text.Json;
using Xunit;

namespace Pebtos.GatewayApi.Core.Tests
{
    public class UsageDetailsSerializationTests
    {
        [Fact]
        public void SerializeUsageDetailsToJson()
        {
            var usageDetails = new UsageDetails
            {
                Currency = "DKK",
                TotalCost = 0.32m
            };

            var resultJson = @"
{
    ""currency"": ""DKK"",
    ""total_cost"": 0.32
}
";
            AssertionHelpers.AssertTwoJsonStringsAreEqual(resultJson, usageDetails.ToJson());
        }

        [Fact]
        public void DeserializeUsageDetails()
        {
            var usageDetailsJson = @"
{
    ""currency"": ""DKK"",
    ""total_cost"": 0.32
}
";
            var usageDetails = Deserializer.DeserializeUsageDetails(usageDetailsJson);

            AssertionHelpers.AssertTwoJsonStringsAreEqual(usageDetailsJson, usageDetails.ToJson());
        }
    }
}
