using Game.Core.Board;
using Game.Core.GameModes.Rules;
using Game.Core.IO;
using Game.Core.IO.Network;
using Game.Core.Language;
using Game.Core.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Core.GameModes
{
    abstract class AbstractStandardWordSquare : AbstractGameMode
    {
        private readonly IInputOutput _io;
        private readonly IGameRules _gameRules;

        public AbstractStandardWordSquare(
            IInputOutput io,
            TileSchema tileSchema,
            NetworkHost networkHost,
            IGameRules gameRules,
            int bots,
            int players,
            bool showTilePoints,
            int? randomizationSeed = null)
            : base(tileSchema, networkHost, bots, players, showTilePoints, randomizationSeed)
        {
            _io = io;
            _gameRules = gameRules;
        }

        protected override void Setup()
        {
            StartNetworkHost();
            CreateLocalPlayer(_io);
            CreateBots();
            WaitForNetworkPlayers();
        }

        protected override void GameLoop()
        {
            DisplayBoardForAllPlayers();
            while (!_gameRules.BoardHasReachedGameOver(_playerAndBoardCollection.Boards.First()))
            {
                var randomPlayer = PickRandomPlayer();
                SendLetterBeingPickedMessageToAllPlayers(randomPlayer);
                var tile = LetPlayerPickTile(randomPlayer);
                LetAllPlayersPlaceTileOnBoard(tile);
            }
            var scores = CalculateScoreForAllPlayers();
            var leaderboard = GenerateLeaderboard(scores);
            SendInformationToAllPlayers(leaderboard);
            TellAllPlayersGameHasEnded();
        }

        protected override void CleanUp()
        {
            StopNetworkHost();
        }

        private List<KeyValuePair<PlayerBase, int>> CalculateScoreForAllPlayers()
        {
            var scores = new List<KeyValuePair<PlayerBase, int>>(_playerAndBoardCollection.Count);
            foreach (var player in _playerAndBoardCollection.Players)
            {
                var board = _playerAndBoardCollection[player];
                var score = _gameRules.CalculateScore(board);
                scores.Add(new(player, score));
            }
            return scores;
        }

        private void LetAllPlayersPlaceTileOnBoard(Tile tile)
        {
            var placeTileTasks = new Task[_playerAndBoardCollection.Players.Count];
            for (int i = 0; i < _playerAndBoardCollection.Players.Count; i++)
            {
                var player = _playerAndBoardCollection.Players[i];
                var placementTask = LetPlayerPlaceTileOnBoardAsync(player, tile, true);
                placeTileTasks[i] = placementTask;
            }
            Task.WaitAll(placeTileTasks);
        }
    }
}
