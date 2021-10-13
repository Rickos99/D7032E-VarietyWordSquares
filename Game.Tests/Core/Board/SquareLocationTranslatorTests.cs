using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Core.Board.Tests
{
    [TestClass]
    public class SquareLocationTranslatorTests
    {
        static object[] StringLocations
        {
            get
            {
                return new[]
                {
                    new object[] {new SquareLocation(0, 0), "A0"},
                    new object[] {new SquareLocation(1, 1), "B1"},
                    new object[] {new SquareLocation(90, 457), "CM457"},
                };
            }
        }

        static object[] SquareLocations
        {
            get
            {
                return new[]
                {
                    new object[] {"A0", new SquareLocation(0, 0)},
                    new object[] {"B1", new SquareLocation(1, 1) },
                    new object[] { "CM457", new SquareLocation(90, 457)},
                };
            }
        }

        [DataTestMethod]
        [DynamicData("StringLocations")]
        public void TranslateToString_WhenLocationIsValid(SquareLocation sqLoc, string expected)
        {
            var actual = SquareLocationTranslator.TranslateToString(sqLoc.Row, sqLoc.Column);

            actual.Should().BeEquivalentTo(expected);
        }

        [DataTestMethod]
        [DynamicData("SquareLocations")]
        public void TranslateFromString_WhenLocationIsValid(string location, SquareLocation expected)
        {
            var actual = SquareLocationTranslator.TranslateFromString(location);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}