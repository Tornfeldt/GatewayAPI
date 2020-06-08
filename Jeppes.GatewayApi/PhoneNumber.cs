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
        private PhoneNumber(CountryInfo country, string phoneNumberWithoutPrefix)
        {
            Country = country;
            PhoneNumberWithoutPrefix = phoneNumberWithoutPrefix;
        }

        public long ConvertToMSISDN()
        {
            var phoneNumberString = Country.PhonePrefix + PhoneNumberWithoutPrefix;
            return long.Parse(phoneNumberString);
        }

        public CountryInfo Country { get; }

        public string PhoneNumberWithoutPrefix { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PhoneNumber);
        }

        public bool Equals(PhoneNumber other)
        {
            return other != null &&
                   EqualityComparer<CountryInfo>.Default.Equals(Country, other.Country) &&
                   PhoneNumberWithoutPrefix == other.PhoneNumberWithoutPrefix;
        }

        public override int GetHashCode()
        {
            var hashCode = 254555775;
            hashCode = hashCode * -1521134295 + EqualityComparer<CountryInfo>.Default.GetHashCode(Country);
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
            var countryInfo = CountryInfoFactory.CreateFromPhoneNumber(phoneNumber, out var phoneNumberWithoutPrefix);
            return new PhoneNumber(countryInfo, phoneNumberWithoutPrefix);
        }

        public static PhoneNumber Create(long phoneNumber)
        {
            var countryInfo = CountryInfoFactory.CreateFromPhoneNumber(phoneNumber, out var phoneNumberWithoutPrefix);
            return new PhoneNumber(countryInfo, phoneNumberWithoutPrefix);
        }
    }
}
