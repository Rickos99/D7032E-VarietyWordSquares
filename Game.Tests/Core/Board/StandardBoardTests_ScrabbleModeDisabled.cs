using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Core.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.Resources;
using FluentAssertions;

namespace Game.Core.Board.Tests
{
    [TestClass]
    public class StandardBoardTests_ScrabbleModeDisabled
    {
        private Dictionary _dictionary
        {
            get
            {
                return new Dictionary("Test", new List<string> { "aa", "ab", "test", "de", "ei" });
            }
        }

        private Square[,] _predefinedBoard
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
            var board = new StandardBoard(_dictionary, false, 3);
            board.IsFilled().Should().BeFalse();
        }

        [TestMethod]
        public void BoardIsFilledTest_WhenBoardIsFilled()
        {
            var board = new StandardBoard(_dictionary, false, _predefinedBoard);
            board.IsFilled().Should().BeTrue();
        }

        [TestMethod]
        public void GetAllEmptyLocationsTest()
        {
            var board = new StandardBoard(_dictionary, false, 2);
            board.InsertTileAt(new BoardLocation(0, 0), new Tile('a', 1));
            board.InsertTileAt(new BoardLocation(0, 1), new Tile('b', 1));

            var expectedEmptyLocations = new List<BoardLocation>() {
                new BoardLocation(1, 0),
                new BoardLocation(1, 1)
            };
            board.GetAllEmptyLocations().Should().BeEquivalentTo(expectedEmptyLocations);
        }

        [TestMethod]
        public void GetAllWordsTest()
        {
            var board = new StandardBoard(_dictionary, false, _predefinedBoard);
            var expectedWords = new List<KeyValuePair<string, List<Square>>>() {
                new("ab", new List<Square>(){
                    new(SquareType.Regular, new('a', 1)),
                    new(SquareType.Regular, new('b', 1)),
                }),
                new("de", new List<Square>(){
                    new(SquareType.Regular, new('d', 1)),
                    new(SquareType.Regular, new('e', 1)),
                }),
                new("ei", new List<Square>(){
                    new(SquareType.Regular, new('e', 1)),
                    new(SquareType.Regular, new('i', 1)),
                })
            };

            var actualWords = board.GetAllWords();

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
    }
}
