using FluentAssertions;
using Game.Core.Board;
using Game.Core.IO;
using Game.Core.Players;
using Game.Tests.Core.Board.Boards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Game.Core.GameModes.Tests
{
    [TestClass]
    public class PlayerAndBoardCollectionTests
    {
        [TestMethod]
        public void Indexer_ShouldReturnBoard()
        {
            var collection = new PlayerAndBoardCollection(1);
            var player = new HumanPlayer(new GameConsoleIO());
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty, false);

            collection.Add(player, board);

            collection[player].Should().BeEquivalentTo(board);
        }

        [TestMethod]
        public void Add_ShouldIncreaseCount()
        {
            var collection = new PlayerAndBoardCollection();
            var player = new HumanPlayer(new GameConsoleIO());
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty, false);

            collection.Players.Should().BeEmpty();
            collection.Add(player, board);
            collection.Players.Should().HaveCount(1);
            collection.Boards.Should().HaveCount(1);
            collection.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddDuplicatePlayer_ShouldThrowError()
        {
            var collection = new PlayerAndBoardCollection();
            var player = new HumanPlayer(new GameConsoleIO());
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty, false);

            collection.Add(player, board);
            Assert.ThrowsException<ArgumentException>(() => collection.Add(player, board));
        }

        [TestMethod]
        public void GetNextPlayerTest()
        {
            var collection = new PlayerAndBoardCollection();
            var player1 = new HumanPlayer(new GameConsoleIO());
            var player2 = new HumanPlayer(new GameConsoleIO());
            var player3 = new HumanPlayer(new GameConsoleIO());
            var board = new StandardBoard(PredefinedBoardLayouts.Squares_AllRegular_2x2_Empty, false);

            collection.Add(player1, board);
            collection.Add(player2, board);
            collection.Add(player3, board);

            var lastPlayer = collection.GetNextPlayer();
            var lastPlayerIndex = collection.Players.IndexOf(lastPlayer);

            for (int i = 0; i < collection.Players.Count * 2; i++)
            {
                var nextPlayer = collection.GetNextPlayer();
                var nextPlayerIndex = collection.Players.IndexOf(nextPlayer);

                // Should wrap around and take first player
                if (lastPlayerIndex == collection.Players.Count - 1)
                {
                    nextPlayerIndex.Should().Be(0);
                }
                // Should be next...
                else
                {
                    nextPlayerIndex.Should().Be(lastPlayerIndex + 1);
                }

                lastPlayerIndex = nextPlayerIndex;
            }
        }
    }
}