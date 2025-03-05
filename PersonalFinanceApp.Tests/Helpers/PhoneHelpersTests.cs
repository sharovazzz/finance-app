using PersonalFinanceApp.Helpers;
using PersonalFinanceApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonalFinanceApp.Tests.Helpers
{
    public class PhoneHelpersTests
    {
        [Fact]
        public void TryParseAsPhone_PhoneStartWith7_True()
        {
            string phone = "79961110000";

            var result = PhoneHelpers.TryParseAsPhone(phone, out phone);

            Assert.True(result);
            Assert.Equal("79961110000", phone);
        }

        [Fact]
        public void TryParseAsPhone_PhoneStartWith8_True()
        {
            string phone = "89961110000";

            var result = PhoneHelpers.TryParseAsPhone(phone, out phone);

            Assert.True(result);
            Assert.Equal("79961110000", phone);
        }

        [Fact]
        public void TryParseAsPhone_PhoneWithLengthOf10_True()
        {
            string phone = "9961110000";

            var result = PhoneHelpers.TryParseAsPhone(phone, out phone);

            Assert.True(result);
            Assert.Equal("79961110000", phone);
        }

        [Fact]
        public void TryParseAsPhone_PhoneNotStartWith7And8_False()
        {
            string phone = "39961110000";

            var result = PhoneHelpers.TryParseAsPhone(phone, out phone);

            Assert.False(result);
        }

        [Fact]
        public void TryParseAsPhone_ShortPhone_False()
        {
            string phone = "1110000";

            var result = PhoneHelpers.TryParseAsPhone(phone, out phone);

            Assert.False(result);
        }

        [Fact]
        public void TryParseAsPhone_LongPhone_False()
        {
            string phone = "1119961110000";

            var result = PhoneHelpers.TryParseAsPhone(phone, out phone);

            Assert.False(result);
        }
    }
}