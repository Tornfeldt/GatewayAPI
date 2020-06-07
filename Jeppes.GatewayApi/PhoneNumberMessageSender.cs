using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Jeppes.GatewayApi
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
