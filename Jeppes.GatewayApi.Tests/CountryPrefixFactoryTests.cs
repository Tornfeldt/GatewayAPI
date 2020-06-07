using Jeppes.GatewayApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeppes.GatewayApi.Tests
{
    public class CountryPrefixFactoryTests
    {
        [Fact]
        public void FindCorrectCountryPrefixFromCountryName()
        {
            var d = new Dictionary<string, CountryPrefix>
            {
                { "DK", CountryPrefixes.DK },
                { "No", CountryPrefixes.NO },
                { "uS", CountryPrefixes.US },
                { "gb", CountryPrefixes.GB }
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromShortCountryName(testCase.Key);
                Assert.Equal(testCase.Value, result);
            }
        }

        [Fact]
        public void DoNotFindCountryPrefixFromCountryName()
        {
            var d = new Dictionary<string, CountryPrefix>
            {
                { "No", CountryPrefixes.DK },
                { "DK", CountryPrefixes.NO },
                { "gb", CountryPrefixes.US },
                { "uS", CountryPrefixes.GB }
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromShortCountryName(testCase.Key);
                Assert.NotEqual(testCase.Value, result);
            }
        }

        [Fact]
        public void FindCountryPrefixFromCountryPhonePrefix()
        {
            var d = new Dictionary<int, CountryPrefix>
            {
                { 45, CountryPrefixes.DK },
                { 47, CountryPrefixes.NO },
                { 1, CountryPrefixes.NORTHAMERICA },
                { 44, CountryPrefixes.GB }
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromCountryPhoneCode(testCase.Key);
                Assert.Equal(testCase.Value, result);
            }
        }

        [Fact]
        public void DoNotFindCountryPrefixFromCountryPhonePrefix()
        {
            var d = new Tuple<int, CountryPrefix>[]
            {
                new Tuple<int, CountryPrefix>(47, CountryPrefixes.DK),
                new Tuple<int, CountryPrefix>(45, CountryPrefixes.NO),
                new Tuple<int, CountryPrefix>(44, CountryPrefixes.NORTHAMERICA),
                new Tuple<int, CountryPrefix>(1, CountryPrefixes.GB),
                new Tuple<int, CountryPrefix>(1, CountryPrefixes.US),
                new Tuple<int, CountryPrefix>(1, CountryPrefixes.CA)
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromCountryPhoneCode(testCase.Item1);
                Assert.NotEqual(testCase.Item2, result);
            }
        }

        [Fact]
        public void FindCountryPrefixFromStringCountryPhonePrefix()
        {
            var d = new Dictionary<string, CountryPrefix>
            {
                { "45", CountryPrefixes.DK },
                { "0045", CountryPrefixes.DK },
                { "+45", CountryPrefixes.DK },
                { "1", CountryPrefixes.NORTHAMERICA },
                { "001", CountryPrefixes.NORTHAMERICA },
                { "+1", CountryPrefixes.NORTHAMERICA }
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromCountryPhoneCode(testCase.Key);
                Assert.Equal(testCase.Value, result);
            }
        }

        [Fact]
        public void CreateFromPhoneNumberInt()
        {
            var d = new Tuple<long, CountryPrefix, string>[]
            {
                new Tuple<long, CountryPrefix, string>(4529641389, CountryPrefixes.DK, "29641389"),
                new Tuple<long, CountryPrefix, string>(129641389, CountryPrefixes.NORTHAMERICA, "29641389"),
                new Tuple<long, CountryPrefix, string>(448888888888, CountryPrefixes.GB, "8888888888"),
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromPhoneNumber(testCase.Item1, out var phoneNumber);
                Assert.Equal(testCase.Item2, result);
                Assert.Equal(testCase.Item3, phoneNumber);
            }
        }

        [Fact]
        public void CreateFromPhoneNumberString()
        {
            var d = new Tuple<string, CountryPrefix, string>[]
            {
                new Tuple<string, CountryPrefix, string>("4529641389", CountryPrefixes.DK, "29641389"),
                new Tuple<string, CountryPrefix, string>("004529641389", CountryPrefixes.DK, "29641389"),
                new Tuple<string, CountryPrefix, string>("+4529641389", CountryPrefixes.DK, "29641389"),
                new Tuple<string, CountryPrefix, string>("00129641389", CountryPrefixes.NORTHAMERICA, "29641389"),
                new Tuple<string, CountryPrefix, string>("+129641389", CountryPrefixes.NORTHAMERICA, "29641389"),
                new Tuple<string, CountryPrefix, string>("129641389", CountryPrefixes.NORTHAMERICA, "29641389"),
                new Tuple<string, CountryPrefix, string>("448888888888", CountryPrefixes.GB, "8888888888"),
                new Tuple<string, CountryPrefix, string>("00448888888888", CountryPrefixes.GB, "8888888888"),
                new Tuple<string, CountryPrefix, string>("+448888888888", CountryPrefixes.GB, "8888888888"),
            };

            foreach (var testCase in d)
            {
                var result = CountryPrefixFactory.CreateFromPhoneNumber(testCase.Item1, out var phoneNumber);
                Assert.Equal(testCase.Item2, result);
                Assert.Equal(testCase.Item3, phoneNumber);
            }
        }

        [Fact]
        public void ExceptionWhenCreatingFromPhoneNumberInt()
        {
            var d = new [] { 2141254778, 217534893 };

            foreach (var testCase in d)
            {
                Assert.Throws<PhoneNumberNotValidException>(() => CountryPrefixFactory.CreateFromPhoneNumber(testCase, out var phoneNumber));
            }
        }
    }
}
