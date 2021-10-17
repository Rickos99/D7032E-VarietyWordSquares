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

        static object[] LocationStrings
        {
            get
            {
                return new[] {
                    new object[]{ "A0", true },
                    new object[]{ "AAa01", true },
                    new object[]{ "A$0", false },
                    new object[]{ "A0A", false },
                    new object[]{ "0A0", false },
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(StringLocations))]
        public void TranslateToString_WhenLocationIsValid(SquareLocation sqLoc, string expected)
        {
            var actual = BoardLocationTranslator.TranslateToString(sqLoc.Row, sqLoc.Column);

            actual.Should().BeEquivalentTo(expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(SquareLocations))]
        public void TranslateFromString_WhenLocationIsValid(string location, SquareLocation expected)
        {
            var actual = BoardLocationTranslator.TranslateFromString(location);

            actual.Should().BeEquivalentTo(expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(LocationStrings))]
        public void LocationStringIsValidTest(string location, bool expected)
        {
            BoardLocationTranslator.LocationStringIsValid(location).Should().Be(expected);
        }
    }
}