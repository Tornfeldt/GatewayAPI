using Jeppes.GatewayApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Jeppes.GatewayApi
{
    public static class CountryPrefixFactory
    {
        public static CountryPrefix CreateFromShortCountryName(string shortCountryName)
        {
            if (string.IsNullOrWhiteSpace(shortCountryName))
                throw new ArgumentNullException(nameof(shortCountryName));

            shortCountryName = shortCountryName.Trim().ToUpperInvariant();

            if (CountryPrefixesByShortCountryName.TryGetValue(shortCountryName, out var prefix))
                return prefix;
            else
                throw new ArgumentException($"{shortCountryName} must be a valid short country name.", nameof(shortCountryName));
        }

        public static CountryPrefix CreateFromCountryPhoneCode(int countryPhoneCode)
        {
            if (CountryPrefixesByPhoneCode.TryGetValue(countryPhoneCode, out var prefix))
                return prefix;
            else
                throw new ArgumentException($"{countryPhoneCode} must be a valid country prefix.", nameof(countryPhoneCode));
        }

        public static CountryPrefix CreateFromCountryPhoneCode(string countryPhoneCode)
        {
            if (string.IsNullOrWhiteSpace(countryPhoneCode))
                throw new ArgumentNullException(nameof(countryPhoneCode));

            countryPhoneCode = countryPhoneCode.Trim().TrimStart('+', '0');

            if (countryPhoneCode != string.Empty && int.TryParse(countryPhoneCode, out var countryPhoneCodeInt))
                return CreateFromCountryPhoneCode(countryPhoneCodeInt);
            else
                throw new ArgumentException($"{countryPhoneCode} must be a valid country prefix.", nameof(countryPhoneCode));
        }

        public static CountryPrefix CreateFromPhoneNumber(string phoneNumberWithCountryPrefix, out string phoneNumberWithoutCountryPrefix)
        {
            if (string.IsNullOrWhiteSpace(phoneNumberWithCountryPrefix))
                throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);

            phoneNumberWithCountryPrefix = phoneNumberWithCountryPrefix.Trim().TrimStart('+', '0');

            if (phoneNumberWithCountryPrefix != string.Empty && long.TryParse(phoneNumberWithCountryPrefix, out var phoneNumberWithCountryPrefixInt))
                return CreateFromPhoneNumber(phoneNumberWithCountryPrefixInt, out phoneNumberWithoutCountryPrefix);
            else
                throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);
        }

        public static CountryPrefix CreateFromPhoneNumber(long phoneNumberWithCountryPrefix, out string phoneNumberWithoutCountryPrefix)
        {
            if (phoneNumberWithCountryPrefix < 1000)
                throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);

            var phoneNumberPrefixString = phoneNumberWithCountryPrefix.ToString(CultureInfo.InvariantCulture).Substring(0, 4);

            do
            {
                var phoneNumberPrefix = int.Parse(phoneNumberPrefixString);
                var validPrefixes = CountryPrefixes.All.Where(x => x.CountryPhoneCode == phoneNumberPrefix).ToArray();

                CountryPrefix result = null;
                if (validPrefixes.Length == 1)
                {
                    result = validPrefixes[0];
                }
                if (validPrefixes.Length > 1 && validPrefixes.All(x => x.CountryPhoneCode == CountryPrefixes.NORTHAMERICA.CountryPhoneCode))
                {
                    result = CountryPrefixes.NORTHAMERICA;
                }

                if (result != null)
                {
                    phoneNumberWithoutCountryPrefix = phoneNumberWithCountryPrefix.ToString(CultureInfo.InvariantCulture).Substring(phoneNumberPrefixString.Length);
                    if (string.IsNullOrWhiteSpace(phoneNumberWithoutCountryPrefix))
                        throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);
                    return result;
                }

                phoneNumberPrefixString = phoneNumberPrefixString.Substring(0, phoneNumberPrefixString.Length - 1);
            } while (phoneNumberPrefixString.Length > 0);

            throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);
        }

        private static IImmutableDictionary<string, CountryPrefix> CountryPrefixesByShortCountryName { get; } = CountryPrefixes.All.ToImmutableDictionary(x => x.CountryNameShort);

        private static IImmutableDictionary<int, CountryPrefix> CountryPrefixesByPhoneCode { get; } = CountryPrefixes.All
                                                                                                                     .Where(x => x.CountryPhoneCode != CountryPrefixes.NORTHAMERICA.CountryPhoneCode || x.CountryNameShort == CountryPrefixes.NORTHAMERICA.CountryNameShort)
                                                                                                                     .ToImmutableDictionary(x => x.CountryPhoneCode);
    }
}
