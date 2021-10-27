using FluentAssertions;
using Game.Core.Board;
using Game.Core.Exceptions;
using Game.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Game.Core.Resources.Tests
{
    [TestClass]
    public class SquareTypeSequenceLoaderTests
    {

        [TestMethod]
        public void LoadFromFileTest_CorrectFormat()
        {
            var expected = new SquareType[3, 3];
            expected[0, 0] = SquareType.DoubleWord;
            expected[0, 1] = SquareType.Regular;
            expected[0, 2] = SquareType.TrippleWord;

            expected[1, 0] = SquareType.Regular;
            expected[1, 1] = SquareType.DoubleLetter;
            expected[1, 2] = SquareType.Regular;

            expected[2, 0] = SquareType.TripleLetter;
            expected[2, 1] = SquareType.Regular;
            expected[2, 2] = SquareType.TrippleWord;

            var filePath = Path.Combine(TestResourceLocator.Location, "boardLayout_test.txt");
            var actual = new SquareTypeSequenceLoader().LoadFromFile(filePath);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void LoadFromFileTest_InvalidSize()
        {
            var filePath = Path.Combine(TestResourceLocator.Location, "boardLayout_test_invalidSize.txt");

            Assert.ThrowsException<SquareSequenceFormatException>(() => new SquareTypeSequenceLoader().LoadFromFile(filePath));
        }
    }
}