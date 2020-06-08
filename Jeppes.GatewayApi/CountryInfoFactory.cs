using Jeppes.GatewayApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Jeppes.GatewayApi
{
    public static class CountryInfoFactory
    {
        public static CountryInfo CreateFromCountryNameAbbr(string countryNameAbbr)
        {
            if (string.IsNullOrWhiteSpace(countryNameAbbr))
                throw new ArgumentNullException(nameof(countryNameAbbr));

            countryNameAbbr = countryNameAbbr.Trim().ToUpperInvariant();

            if (CountriesByCountryNameAbbr.TryGetValue(countryNameAbbr, out var countryInfo))
                return countryInfo;
            else
                throw new ArgumentException($"{countryNameAbbr} must be a valid country name abbreviation.", nameof(countryNameAbbr));
        }

        public static CountryInfo CreateFromCountryPhoneCode(int countryPhonePrefix)
        {
            if (CountriesByPhonePrefix.TryGetValue(countryPhonePrefix, out var countryInfo))
                return countryInfo;
            else
                throw new ArgumentException($"{countryPhonePrefix} must be a valid country phone prefix.", nameof(countryPhonePrefix));
        }

        public static CountryInfo CreateFromCountryPhonePrefix(string countryPhonePrefix)
        {
            if (string.IsNullOrWhiteSpace(countryPhonePrefix))
                throw new ArgumentNullException(nameof(countryPhonePrefix));

            countryPhonePrefix = countryPhonePrefix.Trim().TrimStart('+', '0');

            if (countryPhonePrefix != string.Empty && int.TryParse(countryPhonePrefix, out var countryPhonePrefixInt))
                return CreateFromCountryPhoneCode(countryPhonePrefixInt);
            else
                throw new ArgumentException($"{countryPhonePrefix} must be a valid country phone prefix.", nameof(countryPhonePrefix));
        }

        public static CountryInfo CreateFromPhoneNumber(string phoneNumberWithCountryPrefix, out string phoneNumberWithoutCountryPrefix)
        {
            if (string.IsNullOrWhiteSpace(phoneNumberWithCountryPrefix))
                throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);

            phoneNumberWithCountryPrefix = phoneNumberWithCountryPrefix.Trim().TrimStart('+', '0');

            if (phoneNumberWithCountryPrefix != string.Empty && long.TryParse(phoneNumberWithCountryPrefix, out var phoneNumberWithCountryPrefixInt))
                return CreateFromPhoneNumber(phoneNumberWithCountryPrefixInt, out phoneNumberWithoutCountryPrefix);
            else
                throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);
        }

        public static CountryInfo CreateFromPhoneNumber(long phoneNumberWithCountryPrefix, out string phoneNumberWithoutCountryPrefix)
        {
            if (phoneNumberWithCountryPrefix < 1000)
                throw new PhoneNumberNotValidException(nameof(phoneNumberWithCountryPrefix), phoneNumberWithCountryPrefix);

            var phoneNumberPrefixString = phoneNumberWithCountryPrefix.ToString(CultureInfo.InvariantCulture).Substring(0, 4);

            do
            {
                var phoneNumberPrefix = int.Parse(phoneNumberPrefixString);
                var validCountries = CountryInfos.All.Where(x => x.PhonePrefix == phoneNumberPrefix).ToArray();

                CountryInfo result = null;
                if (validCountries.Length == 1)
                {
                    result = validCountries[0];
                }
                if (validCountries.Length > 1 && validCountries.All(x => x.PhonePrefix == CountryInfos.NORTHAMERICA.PhonePrefix))
                {
                    result = CountryInfos.NORTHAMERICA;
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

        private static IImmutableDictionary<string, CountryInfo> CountriesByCountryNameAbbr { get; } = CountryInfos.All.ToImmutableDictionary(x => x.NameAbbreviation);

        private static IImmutableDictionary<int, CountryInfo> CountriesByPhonePrefix { get; } = CountryInfos.All
                                                                                                            .Where(x => x.PhonePrefix != CountryInfos.NORTHAMERICA.PhonePrefix || x.NameAbbreviation == CountryInfos.NORTHAMERICA.NameAbbreviation)
                                                                                                            .ToImmutableDictionary(x => x.PhonePrefix);
    }
}
