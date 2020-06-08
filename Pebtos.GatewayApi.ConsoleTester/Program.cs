using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pebtos.GatewayApi.ConsoleTester
{
    class Program
    {
        private static string ApiKey;

        static void Main(string[] args)
        {
            ApiKey = args.FirstOrDefault();

            //SendOnePartSms("Me", "+4529641389", "testing").Wait();
            //SendTwoOnePartSms("Me", "+4529641389", "testing1", "Me", "+4529641389", "testing2").Wait();
        }

        public static async Task SendOnePartSms(string sender, string receiver, string content)
        {
            var smsMessage = new Message
            {
                MaximumNumberOfParts = 1,
                MessageContent = content,
                Sender = new StringMessageSender(sender),
                Recipients = new Recipient[]
                {
                    new Recipient
                    {
                        PhoneNumber = PhoneNumber.Create(receiver)
                    }
                }
            };

            var options = new ApiSettings
            {
                ApiToken = ApiKey
            };

            var apiHandler = new ApiHandler(options);

            var response = await apiHandler.SendMessageAsync(smsMessage);

            Console.WriteLine(response.MessageResponseDetails.MessageId);
        }

        public static async Task SendTwoOnePartSms(string sender1, string receiver1, string content1, string sender2, string receiver2, string content2)
        {
            var smsMessage1 = new Message
            {
                MaximumNumberOfParts = 1,
                MessageContent = content1,
                Sender = new StringMessageSender(sender1),
                Recipients = new Recipient[]
                {
                    new Recipient
                    {
                        PhoneNumber = PhoneNumber.Create(receiver1)
                    }
                }
            };
            var smsMessage2 = new Message
            {
                MaximumNumberOfParts = 1,
                MessageContent = content2,
                Sender = new StringMessageSender(sender2),
                Recipients = new Recipient[]
                {
                    new Recipient
                    {
                        PhoneNumber = PhoneNumber.Create(receiver2)
                    }
                }
            };

            var options = new ApiSettings
            {
                ApiToken = ApiKey
            };

            var apiHandler = new ApiHandler(options);

            var response = await apiHandler.SendMessagesAsync(smsMessage1, smsMessage2);

            Console.WriteLine(response.MessageResponseDetails[0].MessageId);
            Console.WriteLine(response.MessageResponseDetails[1].MessageId);
        }
    }
}
