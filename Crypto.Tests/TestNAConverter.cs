using Crypto.Lib.Json.Converters;
using NUnit.Framework;

namespace Crypto.Tests
{
    [TestFixture]
    public class TestNAConverter
    {
        [Test]
        public void CanConvert()
        {
            var c = new NAConverter();
            Assert.True(c.CanConvert(typeof(long?)));
            Assert.True(c.CanConvert(typeof(decimal?)));
            Assert.True(c.CanConvert(typeof(double?)));
            Assert.False(c.CanConvert(typeof(long)));
            Assert.False(c.CanConvert(typeof(decimal)));
            Assert.False(c.CanConvert(typeof(double)));
        }
    }
}
