using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Jeppes.GatewayApi.Tests
{
    public static class AssertionHelpers
    {
        public static void AssertTwoJsonStringsAreEqual(string json1, string json2)
        {
            var comparer = new JsonElementComparer();

            using var doc1 = JsonDocument.Parse(json1);
            using var doc2 = JsonDocument.Parse(json2);

            Assert.True(comparer.Equals(doc1.RootElement, doc2.RootElement));
        }
    }
}
