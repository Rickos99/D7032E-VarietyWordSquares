using Game.Core.Board;
using Game.Core.Board.Builders;
using Game.Core.IO;
using Game.Core.Network;
using Game.Core.Players;
using Game.Core.Communication;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Game.Core.Language;

namespace Game.Core.GameModes
{
    class StandardWordSquare
    {
        private readonly IInputOutput _io;
        private readonly Dictionary _dictionary;
        private readonly TileSchema _tileSchema;
        private readonly int _numberOfbots;
        private readonly int _numberOfPlayers;
        private readonly int? _botseed;
        private readonly Host _netHost;
        private readonly Random _rng = new Random();
        private readonly int _numberOfBoardRows;
        private readonly int _numberOfBoardColumns;
        private readonly PlayerAndBoardCollection _playerAndBoardCollection;

        private int _pickedLetters = 0;

        public StandardWordSquare(
            IInputOutput io,
            Dictionary dictionary,
            TileSchema tileSchema,
            Host networkHost,
            int bots,
            int players,
            int rows,
            int columns,
            int? botSeed)
        {
            _io = io;
            _dictionary = dictionary;
            _tileSchema = tileSchema;
            _netHost = networkHost;
            _numberOfbots = bots;
            _numberOfPlayers = players;
            _botseed = botSeed;
            _numberOfBoardRows = rows;
            _numberOfBoardColumns = columns;

            _playerAndBoardCollection = new PlayerAndBoardCollection(bots+players);
        }

        public void Start()
        {
            StartNetworkHost();
            CreateLocalPlayer();
            CreateBots();
            WaitForPlayers();
            DisplayBoardForAllPlayers();
            while (!GameFinishedConditionIsMet())
            {
                PlayerBase player = PickRandomPlayer();
                Tile tile = LetPlayerPickTile(player);
                LetPlayersPlaceTileOnBoard(tile);
                _pickedLetters++;
            }
            var scores = CalculateScores();
            DisplayLeaderboard(scores);
            StopGame();
        }

        private void StartNetworkHost()
        {
            _netHost.Start();
        }

        private void CreateBots()
        {
            for (int i = 0; i < _numberOfbots; i++)
            {
                var board = BuildBoard();
                var seed = _botseed ?? _rng.Next();
                _playerAndBoardCollection.Add(new BotPlayer(board, seed), board);
            }
        }

        private void CreateLocalPlayer()
        {
            var board = BuildBoard();
            var player = new LocalPlayer(_io);
            _playerAndBoardCollection.Add(player, board);
        }

        private void WaitForPlayers()
        {
            for (int i = 0; i < _numberOfPlayers - 1; i++)
            {
                var board = BuildBoard();
                var netClient = _netHost.WaitForIncomingConnection();
                var player = new NetworkPlayer(netClient);
                _playerAndBoardCollection.Add(player, board);

                SendMessageToAllPlayers(new InformationMessage($"Player {player.ID} connected"));
            }
        }

        private bool GameFinishedConditionIsMet()
        {
            return _pickedLetters >= _numberOfBoardRows*_numberOfBoardColumns;
        }

        private void DisplayBoardForAllPlayers()
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                var boardString = _playerAndBoardCollection[player].GetBoardAsString(false);
                player.SendMessage(new InformationMessage(boardString));
            }
        }

        private void SendMessageToAllPlayers(IMessage message)
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                player.SendMessage(message);
            }
        }

        private PlayerBase PickRandomPlayer()
        {
            var playerIndex = _rng.Next(0, _playerAndBoardCollection.Count);
            var players = _playerAndBoardCollection.Players;
            return players[playerIndex];
        }

        private Tile LetPlayerPickTile(PlayerBase player)
        {
            return player.PickTile(_tileSchema);
        }

        private void LetPlayersPlaceTileOnBoard(Tile tile)
        {
            var tasks = new Task[_playerAndBoardCollection.Players.Count];
            for (int i = 0; i < _playerAndBoardCollection.Count; i++)
            {
                var player = _playerAndBoardCollection.Players[i];
                var task = Task.Run(() => LetPlayerPlaceTileOnBoard(player, tile));
                tasks[i] = task;
            }
            Task.WaitAll(tasks);
        }

        private void LetPlayerPlaceTileOnBoard(PlayerBase player, Tile tile)
        {
            var location = player.PickTileLocation(tile);
            var board = _playerAndBoardCollection[player];
            while (!board.LocationIsPresentOnBoard(location) || !board.LocationIsEmpty(location))
            {
                player.SendMessage(new InformationMessage($"The placement of {tile.Letter} is invalid, please place it somewhere else."));
                location = player.PickTileLocation(tile);
            }

            board.InsertTileAt(location, tile);
            player.SendMessage(new InformationMessage(board.GetBoardAsString(false)));
            if(_playerAndBoardCollection.Players.Count > 1)
            {
                player.SendMessage(new InformationMessage("Waiting for opponents"));
            }
        }

        private List<KeyValuePair<PlayerBase, int>> CalculateScores()
        {
            var scores = new List<KeyValuePair<PlayerBase, int>>(_playerAndBoardCollection.Count);
            foreach (var player in _playerAndBoardCollection.Players)
            {
                var squareSequences = _playerAndBoardCollection[player].GetAllSquareSequences();
                var words = squareSequences.Where(sequence => _dictionary.ContainsSquareSequence(sequence));

                int score = 0;
                foreach (var word in words)
                {
                    if(word.Count < 3) continue;
                    if (word.Count == 3)
                    {
                        score += 3;
                    }
                    else if (word.Count > 3) {
                        score += (word.Count - 3) * 2;
                    }
                }
                scores.Add(new(player, score));
            }
            return scores;
        }

        private void DisplayLeaderboard(List<KeyValuePair<PlayerBase, int>> scores)
        {
            var winners = scores.OrderByDescending((s) => s.Value).ToArray();
            var leaderboard = new StringBuilder();
            leaderboard.AppendLine("  Leaderboard  ");
            for (int i = 0; i < winners.Length; i++)
            {
                leaderboard.AppendLine($"{i+1}. {winners[i].Key.ID} ({winners[i].Value} points)");
            }
            leaderboard.AppendLine("\nThanks for playing!");
            SendMessageToAllPlayers(new InformationMessage(leaderboard.ToString()));
        }

        private void StopGame()
        {
            _netHost.DisconnectAllClients();
            _netHost.Stop();
        }

        private StandardBoard BuildBoard()
        {
            return BoardBuilder.CreateStandardBuilder(_numberOfBoardRows, _numberOfBoardColumns)
                .UseUniformLayout(SquareType.Regular)
                .Build();
        }
    }
}
