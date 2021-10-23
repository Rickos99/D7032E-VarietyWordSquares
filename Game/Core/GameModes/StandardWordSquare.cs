using Game.Core.Board;
using Game.Core.IO;
using Game.Core.Network;
using Game.Core.Players;
using Game.Core.Resources;
using Game.Core.Communication;
using System;
using System.Threading.Tasks;

namespace Game.Core.GameModes
{
    class StandardWordSquare
    {
        private readonly IInputOutput _io;
        private readonly Dictionary _dictionary;
        private readonly TileSchema _tileSchema;
        private readonly Square[,] _boardLayout;
        private readonly int _numberOfbots;
        private readonly int _numberOfPlayers;
        private readonly int? _botseed;
        private readonly Host _netHost;
        private readonly Random _rng = new Random();
        private readonly PlayerAndBoardCollection _playerAndBoardCollection;

        private int _pickedLetters = 0;

        public StandardWordSquare(
            IInputOutput io,
            Dictionary dictionary,
            TileSchema tileSchema,
            Square[,] boardLayout,
            Host networkHost,
            int bots,
            int players,
            int? botSeed)
        {
            _io = io;
            _dictionary = dictionary;
            _tileSchema = tileSchema;
            _boardLayout = boardLayout;
            _netHost = networkHost;
            _numberOfbots = bots;
            _numberOfPlayers = players;
            _botseed = botSeed;

            _playerAndBoardCollection = new PlayerAndBoardCollection(bots+players);
        }

        public void Start()
        {
            StartNetworkHost();
            CreateLocalPlayer();
            CreateBots();
            WaitForPlayers();
            while (!GameFinishedConditionIsMet())
            {
                DisplayBoardForAllPlayers();
                PlayerBase player = PickRandomPlayer();
                Tile tile = LetPlayerPickTile(player);
                LetPlayersPlaceTileOnBoard(tile);
                _pickedLetters++;
            }
            DisplayWinner();
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
                var board = new StandardBoard(_dictionary, false, _boardLayout);
                var seed = _botseed ?? _rng.Next();
                _playerAndBoardCollection.Add(new BotPlayer(board, seed), board);
            }
        }

        private void CreateLocalPlayer()
        {
            var board = new StandardBoard(_dictionary, false, _boardLayout);
            var player = new LocalPlayer(_io);
            _playerAndBoardCollection.Add(player, board);
        }

        private void WaitForPlayers()
        {
            for (int i = 0; i < _numberOfPlayers - 1; i++)
            {
                var board = new StandardBoard(_dictionary, false, _boardLayout);
                var netClient = _netHost.WaitForIncomingConnection();
                var player = new NetworkPlayer(netClient);
                _playerAndBoardCollection.Add(player, board);

                SendMessageToAllPlayers(new InformationMessage($"Player {player.ID} connected"));
            }
        }

        private bool GameFinishedConditionIsMet()
        {
            return _pickedLetters >= _boardLayout.Length;
        }

        private void DisplayBoardForAllPlayers()
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                var boardString = _playerAndBoardCollection[player].GetBoardAsString();
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
            while (!board.LocationIsPresentOnBoard(location))
            {
                player.SendMessage(new InformationMessage($"The placement of {tile.Letter} is not on board, please place it somewhere else."));
                location = player.PickTileLocation(tile);
            }

            _playerAndBoardCollection[player].InsertTileAt(location, tile);
            if(_playerAndBoardCollection.Players.Count > 1)
            {
                player.SendMessage(new InformationMessage("Waiting for opponents"));
            }
        }

        private void DisplayWinner()
        {
            //throw new NotImplementedException();
            System.Diagnostics.Debug.WriteLine("Finished a round of Standard Word Square. No winners");
        }

        private void StopGame()
        {
            _netHost.DisconnectAllClients();
            _netHost.Stop();
        }
    }
}
