using Jeppes.GatewayApi.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Jeppes.GatewayApi
{
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public class PhoneNumber : IEquatable<PhoneNumber>
    {
        private PhoneNumber(CountryPrefix countryPrefix, string phoneNumberWithoutPrefix)
        {
            CountryPrefix = countryPrefix;
            PhoneNumberWithoutPrefix = phoneNumberWithoutPrefix;
        }

        public long ConvertToMSISDN()
        {
            var phoneNumberString = CountryPrefix.CountryPhoneCode + PhoneNumberWithoutPrefix;
            return long.Parse(phoneNumberString);
        }

        public CountryPrefix CountryPrefix { get; }

        public string PhoneNumberWithoutPrefix { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PhoneNumber);
        }

        public bool Equals(PhoneNumber other)
        {
            return other != null &&
                   EqualityComparer<CountryPrefix>.Default.Equals(CountryPrefix, other.CountryPrefix) &&
                   PhoneNumberWithoutPrefix == other.PhoneNumberWithoutPrefix;
        }

        public override int GetHashCode()
        {
            var hashCode = 254555775;
            hashCode = hashCode * -1521134295 + EqualityComparer<CountryPrefix>.Default.GetHashCode(CountryPrefix);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumberWithoutPrefix);
            return hashCode;
        }

        public static bool operator ==(PhoneNumber left, PhoneNumber right)
        {
            return EqualityComparer<PhoneNumber>.Default.Equals(left, right);
        }

        public static bool operator !=(PhoneNumber left, PhoneNumber right)
        {
            return !(left == right);
        }


        public static PhoneNumber Create(string phoneNumber)
        {
            var prefix = CountryPrefixFactory.CreateFromPhoneNumber(phoneNumber, out var phoneNumberWithoutPrefix);
            return new PhoneNumber(prefix, phoneNumberWithoutPrefix);
        }

        public static PhoneNumber Create(long phoneNumber)
        {
            var prefix = CountryPrefixFactory.CreateFromPhoneNumber(phoneNumber, out var phoneNumberWithoutPrefix);
            return new PhoneNumber(prefix, phoneNumberWithoutPrefix);
        }
    }
}
