using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Util.Extensions.Tests
{
    [TestClass]
    public class IntegerExtensionsTests
    {
        static object[] Integers
        {
            get
            {
                return new[]
                {
                    new object[] {39, "AN"},
                    new object[] {26, "AA"},
                    new object[] {0, "A"},
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Integers))]
        public void ConvertToAlphabeticTest(int integer, string excpected)
        {
            integer.ConvertToAlphabetic().Should().Be(excpected);
        }
    }
}