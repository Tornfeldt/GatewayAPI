using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pebtos.GatewayApi.Core.Exceptions
{
    public class PhoneNumberNotValidException : ArgumentException
    {
        internal PhoneNumberNotValidException(string paramName, string phoneNumber)
            : base($"{phoneNumber} is not a valid phone number with a valid prefix.", paramName)
        { }

        internal PhoneNumberNotValidException(string paramName, long phoneNumber)
            : base($"{phoneNumber.ToString(CultureInfo.InvariantCulture)} is not a valid phone number with a valid prefix.", paramName)
        { }
    }
}
