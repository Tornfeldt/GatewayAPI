using System;
using System.Collections.Generic;
using System.Text;

namespace Jeppes.GatewayApi.Exceptions
{
    public class ApiException : Exception
    {
        private static string GenerateErrorMessage(int httpStatusCode, string details)
        {
            return $"HTTP status code: {httpStatusCode}.{(details == null ? string.Empty : $" Details: {details}")}";
        }

        internal ApiException(int httpStatusCode, string details) : base(GenerateErrorMessage(httpStatusCode, details))
        {

        }

        internal ApiException(int httpStatusCode, string details, Exception innerException) : base(GenerateErrorMessage(httpStatusCode, details), innerException)
        {

        }
    }
}
