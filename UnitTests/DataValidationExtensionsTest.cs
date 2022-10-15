using FXExchange.Helper;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        [Test]
        public void ValidateISO_ValidISO_ReturnsTrue()
        {
            Assert.IsTrue("EUR".ValidateISO());
        }

        [Test]
        public void ValidateISO_InvalidISO_ReturnsFalse()
        {
            Assert.IsFalse("eur".ValidateISO());
            Assert.IsFalse("euro".ValidateISO());
        }

        [Test]
        public void ValidateExchangeAmount_InvalidAmount_ReturnsFalse()
        {
            Assert.IsFalse(new decimal(-100).ValidateExchangeAmount());
        }

        [Test]
        public void ValidateExchangeAmount_ValidAmount_ReturnsTrue()
        {
            Assert.IsTrue(new decimal(100).ValidateExchangeAmount());
        }
    }
}