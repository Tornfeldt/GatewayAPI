using Pebtos.GatewayApi.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pebtos.GatewayApi.Core
{
    public class PhoneNumberMessageSender : MessageSender
    {
        private readonly PhoneNumber _phoneNumber;

        public PhoneNumberMessageSender(PhoneNumber phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        internal override string GenerateSenderString()
        {
            return _phoneNumber.ConvertToMSISDN().ToString(CultureInfo.InvariantCulture);
        }
    }
}
