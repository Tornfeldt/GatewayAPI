using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Jeppes.GatewayApi
{
    public class CountryPrefix : IEquatable<CountryPrefix>
    {
        internal CountryPrefix(int countryPhoneCode, string countryNameShort, string countryName)
        {
            CountryPhoneCode = countryPhoneCode;
            CountryNameShort = countryNameShort;
            CountryName = countryName;
            CountryPhoneCodeAsString = countryPhoneCode.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The phone number prefix, e.g. 45 for Denmark and 1 for the United States.
        /// </summary>
        public int CountryPhoneCode { get; }

        internal string CountryPhoneCodeAsString { get; }

        /// <summary>
        /// The country code in short form, e.g. DK for Denmark and US for the United States.
        /// </summary>
        public string CountryNameShort { get; }

        /// <summary>
        /// Name of the country.
        /// </summary>
        public string CountryName { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as CountryPrefix);
        }

        public bool Equals(CountryPrefix other)
        {
            return other != null &&
                   CountryPhoneCode == other.CountryPhoneCode &&
                   CountryPhoneCodeAsString == other.CountryPhoneCodeAsString &&
                   CountryNameShort == other.CountryNameShort &&
                   CountryName == other.CountryName;
        }

        public override int GetHashCode()
        {
            var hashCode = 1701420055;
            hashCode = hashCode * -1521134295 + CountryPhoneCode.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CountryPhoneCodeAsString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CountryNameShort);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CountryName);
            return hashCode;
        }

        public static bool operator ==(CountryPrefix left, CountryPrefix right)
        {
            return EqualityComparer<CountryPrefix>.Default.Equals(left, right);
        }

        public static bool operator !=(CountryPrefix left, CountryPrefix right)
        {
            return !(left == right);
        }
    }
}
