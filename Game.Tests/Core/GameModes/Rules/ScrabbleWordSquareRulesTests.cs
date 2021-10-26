using FluentAssertions;
using Game.Core.Board;
using Game.Core.Language;
using Game.Tests.Core.Board.Boards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Game.Core.GameModes.Rules.Tests
{
    [TestClass]
    public class ScrabbleWordSquareRulesTests
    {
        private Dictionary _dictionary
        {
            get
            {
                return new Dictionary("Test", new List<string> { "aa", "test", "aaa" });
            }
        }

        static object[] TestData_NotFilledSquareArrays
        {
            get
            {
                return new[]
                {
                    new object[] {PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty},
                    new object[] {PredefinedBoardLayouts.Squares_AllRegular_2x2_SemiFilled },
                };
            }
        }

        static object[] TestData_ScoreValueOf_FilledSquareArrays
        {
            get
            {
                return new[]
                {
                    new object[] {PredefinedBoardLayouts.Squares_AllRegular_3x3_Filled, 27},
                    new object[] {PredefinedBoardLayouts.Squares_AllRegular_4x4_Filled, 23},
                    new object[] {PredefinedBoardLayouts.Squares_MixedSquareTypes_4x4_Filled, 65}
                };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(TestData_NotFilledSquareArrays))]
        public void BoardHasReachedGameOverTest_NotFilledBoard(Square[,] squares)
        {
            var board = new StandardBoard(squares, default);
            var rules = new ScrabbleWordSquareRules(_dictionary);
            rules.BoardHasReachedGameOver(board).Should().BeFalse();
        }

        [TestMethod]
        public void BoardHasReachedGameOverTest_FilledBoard()
        {
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_3x3_Filled, default);
            var rules = new ScrabbleWordSquareRules(_dictionary);
            rules.BoardHasReachedGameOver(board).Should().BeTrue();
        }

        [DataTestMethod]
        [DynamicData(nameof(TestData_ScoreValueOf_FilledSquareArrays))]
        public void CalculateScoreTest(Square[,] squares, int expectedScore)
        {
            var board = new StandardBoard(squares, default);
            var rules = new ScrabbleWordSquareRules(_dictionary);
            rules.CalculateScore(board).Should().Be(expectedScore);
        }
    }
}