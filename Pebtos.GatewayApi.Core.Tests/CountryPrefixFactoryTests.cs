using Pebtos.GatewayApi.Core;
using Pebtos.GatewayApi.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Pebtos.GatewayApi.Core.Tests
{
    public class CountryPrefixFactoryTests
    {
        [Fact]
        public void FindCorrectCountryPrefixFromCountryName()
        {
            var d = new Dictionary<string, CountryInfo>
            {
                { "DK", CountryInfos.DK },
                { "No", CountryInfos.NO },
                { "uS", CountryInfos.US },
                { "gb", CountryInfos.GB }
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromCountryNameAbbr(testCase.Key);
                Assert.Equal(testCase.Value, result);
            }
        }

        [Fact]
        public void DoNotFindCountryPrefixFromCountryName()
        {
            var d = new Dictionary<string, CountryInfo>
            {
                { "No", CountryInfos.DK },
                { "DK", CountryInfos.NO },
                { "gb", CountryInfos.US },
                { "uS", CountryInfos.GB }
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromCountryNameAbbr(testCase.Key);
                Assert.NotEqual(testCase.Value, result);
            }
        }

        [Fact]
        public void FindCountryPrefixFromCountryPhonePrefix()
        {
            var d = new Dictionary<int, CountryInfo>
            {
                { 45, CountryInfos.DK },
                { 47, CountryInfos.NO },
                { 1, CountryInfos.NORTHAMERICA },
                { 44, CountryInfos.GB }
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromCountryPhoneCode(testCase.Key);
                Assert.Equal(testCase.Value, result);
            }
        }

        [Fact]
        public void DoNotFindCountryPrefixFromCountryPhonePrefix()
        {
            var d = new Tuple<int, CountryInfo>[]
            {
                new Tuple<int, CountryInfo>(47, CountryInfos.DK),
                new Tuple<int, CountryInfo>(45, CountryInfos.NO),
                new Tuple<int, CountryInfo>(44, CountryInfos.NORTHAMERICA),
                new Tuple<int, CountryInfo>(1, CountryInfos.GB),
                new Tuple<int, CountryInfo>(1, CountryInfos.US),
                new Tuple<int, CountryInfo>(1, CountryInfos.CA)
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromCountryPhoneCode(testCase.Item1);
                Assert.NotEqual(testCase.Item2, result);
            }
        }

        [Fact]
        public void FindCountryPrefixFromStringCountryPhonePrefix()
        {
            var d = new Dictionary<string, CountryInfo>
            {
                { "45", CountryInfos.DK },
                { "0045", CountryInfos.DK },
                { "+45", CountryInfos.DK },
                { "1", CountryInfos.NORTHAMERICA },
                { "001", CountryInfos.NORTHAMERICA },
                { "+1", CountryInfos.NORTHAMERICA }
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromCountryPhonePrefix(testCase.Key);
                Assert.Equal(testCase.Value, result);
            }
        }

        [Fact]
        public void CreateFromPhoneNumberInt()
        {
            var d = new Tuple<long, CountryInfo, string>[]
            {
                new Tuple<long, CountryInfo, string>(4529641389, CountryInfos.DK, "29641389"),
                new Tuple<long, CountryInfo, string>(129641389, CountryInfos.NORTHAMERICA, "29641389"),
                new Tuple<long, CountryInfo, string>(448888888888, CountryInfos.GB, "8888888888"),
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromPhoneNumber(testCase.Item1, out var phoneNumber);
                Assert.Equal(testCase.Item2, result);
                Assert.Equal(testCase.Item3, phoneNumber);
            }
        }

        [Fact]
        public void CreateFromPhoneNumberString()
        {
            var d = new Tuple<string, CountryInfo, string>[]
            {
                new Tuple<string, CountryInfo, string>("4529641389", CountryInfos.DK, "29641389"),
                new Tuple<string, CountryInfo, string>("004529641389", CountryInfos.DK, "29641389"),
                new Tuple<string, CountryInfo, string>("+4529641389", CountryInfos.DK, "29641389"),
                new Tuple<string, CountryInfo, string>("00129641389", CountryInfos.NORTHAMERICA, "29641389"),
                new Tuple<string, CountryInfo, string>("+129641389", CountryInfos.NORTHAMERICA, "29641389"),
                new Tuple<string, CountryInfo, string>("129641389", CountryInfos.NORTHAMERICA, "29641389"),
                new Tuple<string, CountryInfo, string>("448888888888", CountryInfos.GB, "8888888888"),
                new Tuple<string, CountryInfo, string>("00448888888888", CountryInfos.GB, "8888888888"),
                new Tuple<string, CountryInfo, string>("+448888888888", CountryInfos.GB, "8888888888"),
            };

            foreach (var testCase in d)
            {
                var result = CountryInfoFactory.CreateFromPhoneNumber(testCase.Item1, out var phoneNumber);
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
                Assert.Throws<PhoneNumberNotValidException>(() => CountryInfoFactory.CreateFromPhoneNumber(testCase, out var phoneNumber));
            }
        }
    }
}
