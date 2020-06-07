using Jeppes.GatewayApi.Helpers;
using System;
using System.Text.Json;
using Xunit;

namespace Jeppes.GatewayApi.Tests
{
    public class MessageSerializationTests
    {
        [Fact]
        public void FullMessageObjectSerializesToJson()
        {
            var message = new Message
            {
                CallbackUrl = "https://example.com/cb?foo=bar",
                Class = MessageClass.Secret,
                MessageContent = "Hello world",
                Payload = "cGF5bG9hZCBlbmNvZGVkIGFzIGI2NAo=",
                Label = "Deathstar inc.",
                Sender = new StringMessageSender("Test Sender"),
                SendTime = new DateTime(2020, 10, 10, 23, 01, 01),
                UserReference = "1234",
                Priority = Priority.Urgent,
                ValidityPeriod = TimeSpan.FromSeconds(78),
                Encoding = SmsEncoding.GSM_03_38,
                Destination = Destination.Mobile,
                UDH = "BQQLhCPw",
                Recipients = new Recipient[]
                {
                    new Recipient
                    {
                        PhoneNumber = PhoneNumber.Create(1514654321)
                    },
                    new Recipient
                    {
                        PhoneNumber = PhoneNumber.Create(1514654322)
                    }
                }
            };

            var messageJson = message.ToJson();
            var resultJson = @"
    {
        ""class"": ""secret"",
        ""message"": ""Hello world"",
        ""payload"": ""cGF5bG9hZCBlbmNvZGVkIGFzIGI2NAo="",
        ""label"": ""Deathstar inc."",
        ""recipients"": [
            {
                ""msisdn"": 1514654321
            },
            {
                ""msisdn"": 1514654322
            }
        ],
        ""sender"": ""Test Sender"",
        ""sendtime"": 1602370861,
        ""userref"": ""1234"",
        ""priority"": ""URGENT"",
        ""validity_period"": 78,
        ""encoding"": ""UTF8"",
        ""destaddr"": ""MOBILE"",
        ""udh"": ""BQQLhCPw"",
        ""callback_url"": ""https://example.com/cb?foo=bar""
    }
";
            AssertionHelpers.AssertTwoJsonStringsAreEqual(messageJson, resultJson);
        }

        [Fact]
        public void SmallMessageObjectSerializesToJson()
        {
            var message = new Message
            {
                Class = MessageClass.Standard,
                MessageContent = "Hello world",
                Sender = new PhoneNumberMessageSender(PhoneNumber.Create("+4529641389")),
                Priority = Priority.Urgent,
                Encoding = SmsEncoding.GSM_03_38,
                Recipients = new Recipient[]
                {
                    new Recipient
                    {
                        PhoneNumber = PhoneNumber.Create(1514654321)
                    }
                }
            };

            var messageJson = message.ToJson();
            var resultJson = @"
    {
        ""class"": ""standard"",
        ""message"": ""Hello world"",
        ""recipients"": [
            {
                ""msisdn"": 1514654321
            }
        ],
        ""sender"": ""4529641389"",
        ""priority"": ""URGENT"",
        ""encoding"": ""UTF8""
    }
";
            AssertionHelpers.AssertTwoJsonStringsAreEqual(messageJson, resultJson);
        }

        [Fact]
        public void SmallMessageDeserializesToMessageAndBack()
        {
            var messageJson = @"
    {
        ""class"": ""standard"",
        ""message"": ""Hello world"",
        ""recipients"": [
            {
                ""msisdn"": 1514654321
            }
        ],
        ""sender"": ""4529641389"",
        ""priority"": ""URGENT"",
        ""encoding"": ""UTF8""
    }
";
            var message = Deserializer.DeserializeMessage(messageJson);

            AssertionHelpers.AssertTwoJsonStringsAreEqual(messageJson, message.ToJson());
        }

        [Fact]
        public void FullMessageDeserializesToMessageAndBack()
        {
            var messageJson = @"
    {
        ""class"": ""secret"",
        ""message"": ""Hello world"",
        ""payload"": ""cGF5bG9hZCBlbmNvZGVkIGFzIGI2NAo="",
        ""label"": ""Deathstar inc."",
        ""recipients"": [
            {
                ""msisdn"": 1514654321
            },
            {
                ""msisdn"": 1514654322
            }
        ],
        ""sender"": ""Test Sender"",
        ""sendtime"": 1602370861,
        ""userref"": ""1234"",
        ""priority"": ""URGENT"",
        ""validity_period"": 78,
        ""encoding"": ""UTF8"",
        ""destaddr"": ""MOBILE"",
        ""udh"": ""BQQLhCPw"",
        ""callback_url"": ""https://example.com/cb?foo=bar""
    }
";
            var message = Deserializer.DeserializeMessage(messageJson);

            AssertionHelpers.AssertTwoJsonStringsAreEqual(messageJson, message.ToJson());
        }
    }
}
