using Pebtos.GatewayApi.Core;
using Pebtos.GatewayApi.Helpers;
using Pebtos.GatewayApi.Core.Helpers;
using System;
using System.Text.Json;
using Xunit;

namespace Pebtos.GatewayApi.Tests
{
    public class MessageStatusSerializationTests
    {
        [Fact]
        public void SerializeMessageStatusToJson()
        {
            var messageStatus = new MessageStatus
            {
                MessageId = 1000001,
                Recipient = PhoneNumber.Create(4587654321),
                SendTimestamp = new DateTime(2020, 10, 10, 23, 01, 01),
                Status = MessageDeliveryStatus.Delivered,
                UserReference = "foobar"
            };

            var resultJson = @"
{
    ""id"": 1000001,
    ""msisdn"": 4587654321,
    ""time"": 1602370861,
    ""status"": ""DELIVERED"",
    ""userref"": ""foobar""
}
";
            AssertionHelpers.AssertTwoJsonStringsAreEqual(resultJson, messageStatus.ToJson());
        }

        [Fact]
        public void DeserializeMessageStatus()
        {
            var messageStatusJson = @"
{
    ""id"": 1000001,
    ""msisdn"": 4587654321,
    ""time"": 1602370861,
    ""status"": ""DELIVERED"",
    ""userref"": ""foobar""
}
";
            var messageStatus = Core.Helpers.Deserializer.DeserializeMessageStatus(messageStatusJson);

            AssertionHelpers.AssertTwoJsonStringsAreEqual(messageStatusJson, messageStatus.ToJson());
        }
    }
}
