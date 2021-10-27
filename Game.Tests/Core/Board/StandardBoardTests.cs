using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Core.Board;
using Game.Tests.Core.Board.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Game.Core.Language;

namespace Game.Core.Board.Tests
{
    [TestClass]
    public class StandardBoardTests
    {
        private Dictionary _dictionary
        {
            get
            {
                return new Dictionary("Test", new List<string> { "aa", "ab", "test", "de", "ei" });
            }
        }

        private Square[,] _predefinedBoard9Squares
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('B', 1)), new(SquareType.Regular, new('C', 1))},
                    { new(SquareType.Regular, new('D', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('F', 1))},
                    { new(SquareType.Regular, new('G', 1)), new(SquareType.Regular, new('H', 1)), new(SquareType.Regular, new('I', 1))},
                };
            }
        }

        [TestMethod]
        public void BoardIsFilledTest_WhenBoardIsEmpty()
        {
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty, default);
            board.IsFilled().Should().BeFalse();
        }

        [TestMethod]
        public void BoardIsFilledTest_WhenBoardIsFilled()
        {
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_3x3_Filled, default);
            board.IsFilled().Should().BeTrue();
        }

        [TestMethod]
        public void GetAllEmptyLocationsTest()
        {
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty, default);
            board.InsertTileAt(new BoardLocation(0, 0), new Tile('a', 1));
            board.InsertTileAt(new BoardLocation(0, 1), new Tile('b', 1));

            var expectedEmptyLocations = new List<BoardLocation>() {
                new BoardLocation(1, 0),
                new BoardLocation(1, 1)
            };
            board.GetAllEmptyLocations().Should().BeEquivalentTo(expectedEmptyLocations);
        }

        [TestMethod]
        public void GetAllSquareSequences()
        {
            var board = new StandardBoard(_predefinedBoard9Squares, default);
            var expectedWords = new List<List<Square>>() {
                new List<Square>(){
                    new(SquareType.Regular, new('a', 1)),
                    new(SquareType.Regular, new('b', 1)),
                },
                new List<Square>(){
                    new(SquareType.Regular, new('d', 1)),
                    new(SquareType.Regular, new('e', 1)),
                },
                new List<Square>(){
                    new(SquareType.Regular, new('e', 1)),
                    new(SquareType.Regular, new('i', 1)),
                }
            };

            var actualWords = board.GetAllSquareSequences().Where(sequence => _dictionary.ContainsSquareSequence(sequence));

            actualWords.Should().BeEquivalentTo(expectedWords);
        }

        [Ignore]
        [TestMethod]
        public void GetBoardAsStringTest()
        {
        }

        [Ignore]
        [TestMethod]
        public void InsertLetterAtTest()
        {
        }

        [TestMethod()]
        public void CopyTest()
        {
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_3x3_Filled, false);
            var boardCopy = board.Copy();

            boardCopy.Should().NotBeSameAs(board);
            boardCopy.GetAllSquareSequences().Should().BeEquivalentTo(board.GetAllSquareSequences());

            boardCopy.InsertTileAt(new BoardLocation(0, 0), new('Z', 1));

            boardCopy.GetAllSquareSequences().Should().NotBeEquivalentTo(board.GetAllSquareSequences());
        }
    }
}
