using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game.Core.GameModes.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.Board;
using Game.Core.Language;
using FluentAssertions;

namespace Game.Core.GameModes.Rules.Tests
{
    [TestClass]
    public class StandardWordSquareRulesTests
    {
        private Dictionary _dictionary
        {
            get
            {
                return new Dictionary("Test", new List<string> { "aa", "ab", "test", "de", "ei", "aaa" });
            }
        }

        private static Square[,] _predefinedSquares_3x3_Filled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('B', 1)), new(SquareType.Regular, new('D', 1))},
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('E', 1))},
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1))},
                };
            }
        }

        private static Square[,] _predefinedSquares_4x4_Filled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular, new('T', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('D', 1)), new(SquareType.Regular, new('T', 1))},
                    { new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('E', 1))},
                    { new(SquareType.Regular, new('S', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('S', 1))},
                    { new(SquareType.Regular, new('T', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('S', 1)), new(SquareType.Regular, new('T', 1))},
                };
            }
        }

        private static Square[,] _predefinedSquares_2x2_SemiFilled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular), new(SquareType.Regular, new('A', 1))},
                    { new(SquareType.Regular), new(SquareType.Regular)},
                };
            }
        }

        private static Square[,] _predefinedSquares_2x2_Empty
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular), new(SquareType.Regular)},
                    { new(SquareType.Regular), new(SquareType.Regular)},
                };
            }
        }

        static object[] TestData_NotFilledSquareArrays
        {
            get
            {
                return new[]
                {
                    new object[] {_predefinedSquares_2x2_Empty},
                    new object[] {_predefinedSquares_2x2_SemiFilled},
                };
            }
        }

        static object[] TestData_ScoreValueOf_FilledSquareArrays
        {
            get
            {
                return new[]
                {
                    new object[] {_predefinedSquares_3x3_Filled, 1},
                    new object[] {_predefinedSquares_4x4_Filled, 3}
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(TestData_NotFilledSquareArrays))]
        public void BoardHasReachedGameOver_NotFilledBoard(Square[,] squares)
        {
            var board = new StandardBoard(squares);
            var rules = new StandardWordSquareRules(_dictionary);
            rules.BoardHasReachedGameOver(board).Should().BeFalse();
        }

        [TestMethod]
        public void BoardHasReachedGameOver_FilledBoard()
        {
            var board = new StandardBoard(_predefinedSquares_3x3_Filled);
            var rules = new StandardWordSquareRules(_dictionary);
            rules.BoardHasReachedGameOver(board).Should().BeTrue();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestData_ScoreValueOf_FilledSquareArrays))]
        public void CalculateScoreTest(Square[,] squares, int score)
        {
            var board = new StandardBoard(squares);
            var rules = new StandardWordSquareRules(_dictionary);
            rules.CalculateScore(board).Should().Be(score);
        }
    }
}