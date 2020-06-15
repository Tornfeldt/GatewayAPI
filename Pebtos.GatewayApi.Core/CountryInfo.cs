using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pebtos.GatewayApi.Core
{
    public class CountryInfo : IEquatable<CountryInfo>
    {
        internal CountryInfo(int phonePrefix, string countryNameAbbreviation, string countryName)
        {
            PhonePrefix = phonePrefix;
            NameAbbreviation = countryNameAbbreviation;
            Name = countryName;
            PhonePrefixAsString = phonePrefix.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The phone number prefix, e.g. 45 for Denmark and 1 for the United States.
        /// </summary>
        public int PhonePrefix { get; }

        internal string PhonePrefixAsString { get; }

        /// <summary>
        /// The country code in short form, e.g. DK for Denmark and US for the United States.
        /// </summary>
        public string NameAbbreviation { get; }

        /// <summary>
        /// Name of the country.
        /// </summary>
        public string Name { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as CountryInfo);
        }

        public bool Equals(CountryInfo other)
        {
            return other != null &&
                   PhonePrefix == other.PhonePrefix &&
                   PhonePrefixAsString == other.PhonePrefixAsString &&
                   NameAbbreviation == other.NameAbbreviation &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1701420055;
            hashCode = hashCode * -1521134295 + PhonePrefix.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhonePrefixAsString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameAbbreviation);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(CountryInfo left, CountryInfo right)
        {
            return EqualityComparer<CountryInfo>.Default.Equals(left, right);
        }

        public static bool operator !=(CountryInfo left, CountryInfo right)
        {
            return !(left == right);
        }
    }
}
