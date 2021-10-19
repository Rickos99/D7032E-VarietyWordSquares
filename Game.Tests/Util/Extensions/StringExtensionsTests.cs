using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Util.Extensions.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        static object[] Strings
        {
            get
            {
                return new[]
                {
                    new object[] {"AN", 39},
                    new object[] {"AA", 26},
                    new object[] {"A", 0},
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Strings))]
        public void TryConvertToIntegerTest(string str, int excpected)
        {
            str.TryConvertToInteger(out int actual);
            actual.Should().Be(excpected);
        }
    }
}