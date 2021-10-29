﻿using Game.Core.Board;
using Game.Core.IO;
using Game.Core.IO.Messages;
using Game.Core.IO.Network;
using Game.Core.Language;
using Game.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.GameModes
{
    /// <summary>
    /// An abstract game mode with commonly used methods used in a gamemode.
    /// </summary>
    abstract class AbstractGameMode
    {
        protected TileSchema _tileSchema;
        protected NetworkHost _netHost;

        /// <summary>
        /// Collection of players and boards. The first player in collection is assumed to be the host of current game.
        /// </summary>
        protected PlayerAndBoardCollection _playerAndBoardCollection = new();
        protected Random _rng;

        protected readonly int _numberOfbots;
        protected readonly int _numberOfPlayers;

        protected readonly bool _showTilePoints;

        /// <summary>
        /// Initialize a new instance of the <see cref="AbstractGameMode"/>
        /// </summary>
        /// <param name="tileSchema">Tileschema to allow players to use.</param>
        /// <param name="networkHost">Network to use when communicating with network players.</param>
        /// <param name="bots">Number of bots to use.</param>
        /// <param name="players">Number of players to allow in game, exluding the host.</param>
        /// <param name="showTilePoints">Indicates whether to print point value of tile. Set to <c>true</c> if points should be shown, otherwise false.</param>
        /// <param name="randomizationSeed">Seed to use in part where randomization is used. Set to null to use a random seed.</param>
        public AbstractGameMode(
            TileSchema tileSchema,
            NetworkHost networkHost,
            int bots,
            int players,
            bool showTilePoints,
            int? randomizationSeed)
        {
            _tileSchema = tileSchema ?? throw new ArgumentNullException(nameof(tileSchema));
            _netHost = networkHost ?? throw new ArgumentNullException(nameof(networkHost));
            _numberOfbots = bots;
            _numberOfPlayers = players;
            _showTilePoints = showTilePoints;
            _rng = randomizationSeed is null ? new Random() : new Random((int)randomizationSeed);
        }

        /// <summary>
        /// Setup the gamemode and allow players to enter, run game until finished,
        /// and cleanup when game is finished.
        /// </summary>
        public virtual void Run()
        {
            Setup();
            GameLoop();
            CleanUp();
        }

        /// <summary>
        /// Create the amount of bots specified by <see cref="_numberOfbots"/> and add
        /// them to <see cref="_playerAndBoardCollection"/>.
        /// </summary>
        /// <remarks>
        /// The bots will be using the board specified in <see cref="BuildBoard()"/> 
        /// and will use the next integer value generated by <see cref="_rng"/> as seed.
        /// </remarks>
        protected virtual void CreateBots()
        {
            for (int i = 0; i < _numberOfbots; i++)
            {
                var board = BuildBoard();
                var seed = _rng.Next();
                var bot = CreateBot(board, seed);
                _playerAndBoardCollection.Add(bot, board);
            }
        }

        /// <summary>
        /// Create a new bot.
        /// </summary>
        /// <param name="board">Board to initialize the bot with.</param>
        /// <param name="seed">Seed to initialize the bot with.</param>
        /// <returns>The created bot.</returns>
        protected virtual BotPlayer CreateBot(StandardBoard board, int seed)
        {
            return new BotPlayer(board, seed);
        }

        /// <summary>
        /// Create a local player and add it to <see cref="_playerAndBoardCollection"/>.
        /// </summary>
        /// <remarks>
        /// The local player will be using the board specified in <see cref="BuildBoard()"/>.
        /// </remarks>
        /// <param name="io">Input-Output interface to create the local player with.</param>
        protected void CreateLocalPlayer(IInputOutput io)
        {
            var player = new LocalPlayer(io);
            var board = BuildBoard();
            _playerAndBoardCollection.Add(player, board);
        }

        /// <summary>
        /// Start the network host.
        /// </summary>
        /// <exception cref="SocketException"></exception>
        protected virtual void StartNetworkHost()
        {
            _netHost.Start();
        }

        /// <summary>
        /// Synchronously wait for the all network players to connect. Players'
        /// board board will be generated by <see cref="BuildBoard"/> method.
        /// </summary>
        protected virtual void WaitForNetworkPlayers()
        {
            if (_numberOfPlayers > 0)
            {
                var waitingForPlayersMessage = new InformationMessage("Waiting for players...");
                _playerAndBoardCollection.Players.FirstOrDefault()?.SendMessage(waitingForPlayersMessage);
            }

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                var board = BuildBoard();
                var netPlayer = WaitForNetworkPlayer();
                netPlayer.SendMessage(new InformationMessage($"You connected to the server as player {netPlayer.ID}"));
                _playerAndBoardCollection.Players.FirstOrDefault()?.SendMessage(new InformationMessage($"Player {netPlayer.ID} connected."));
                _playerAndBoardCollection.Add(netPlayer, board);
            }
        }

        /// <summary>
        /// Synchronously wait for the next network player to connect.
        /// </summary>
        /// <returns>The connected player</returns>
        protected virtual NetworkPlayer WaitForNetworkPlayer()
        {
            var netClient = _netHost.WaitForIncomingConnection();
            return new NetworkPlayer(netClient);
        }

        /// <summary>
        /// Displays the board owned by the specified player to him.
        /// </summary>
        /// <param name="player">Player to display hirs board to.</param>
        /// <param name="displayTilePoints">Inidicates wheher the tilepoints should be showm.</param>
        protected virtual void DisplayBoardForPlayer(PlayerBase player)
        {
            var board = _playerAndBoardCollection[player];
            var boardMessage = new InformationMessage(board.GetBoardAsString());
            player.SendMessage(boardMessage);
        }

        /// <summary>
        /// Goes through all players and sends them a their board.
        /// </summary>
        protected virtual void DisplayBoardForAllPlayers()
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                DisplayBoardForPlayer(player);
            }
        }

        /// <summary>
        /// Pick a random player from <see cref="_playerAndBoardCollection"/>.
        /// </summary>
        /// <returns>A player</returns>
        protected virtual PlayerBase PickRandomPlayer()
        {
            var playerIndex = _rng.Next(0, _playerAndBoardCollection.Count);
            var players = _playerAndBoardCollection.Players;
            return players[playerIndex];
        }

        /// <summary>
        /// Send an <see cref="InformationMessage"/> with content "Waiting for a 
        /// letter to be picked..." to all players. One player will not recieve 
        /// the message and an idea can be to let that user be the player 
        /// which is picking the letter.
        /// </summary>
        /// <param name="playerToExclude">The player to not send the message to.</param>
        protected virtual void SendLetterBeingPickedMessageToAllPlayers(PlayerBase playerToExclude)
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                if (player == playerToExclude) continue;

                player.SendMessage(new InformationMessage("Waiting for a letter to be picked..."));
            }
        }

        /// <summary>
        /// Request the specified player to pick a tile. Before the player gets to pick a tile, the current tileschema (<see cref="_tileSchema"/>) is shown. The tile returned, exists in the <see cref="_tileSchema"/>
        /// </summary>
        /// <param name="player">The player to request a tile from.</param>
        /// <returns>The picked tile</returns>
        protected virtual Tile LetPlayerPickTile(PlayerBase player)
        {
            var tileSchemaString = _showTilePoints ?
                _tileSchema.GetAsStringGroupedByPoints() : _tileSchema.GetAsStringWithOnlyLetters();

            player.SendMessage(new InformationMessage(tileSchemaString));
            return player.PickTile(_tileSchema);
        }

        /// <summary>
        /// Asynchronously request the specified player to pick a tile.
        /// </summary>
        /// <param name="player">The player to request a tile from.</param>
        /// <returns>The picked tile</returns>
        protected Task<Tile> LetPlayerPickTileAsync(PlayerBase player)
        {
            return Task.Run(() => LetPlayerPickTile(player));
        }

        /// <summary>
        /// Let the specified player place a tile on his board.
        /// </summary>
        /// <param name="player">Player to let pick a location for the specifed tile.</param>
        /// <param name="tile">Tile to be placed on player's board.</param>
        /// <param name="displayBoardAfterPlacement">Indicates whether to display the player's board after a tile placement</param>
        protected virtual void LetPlayerPlaceTileOnBoard(PlayerBase player, Tile tile, bool displayBoardAfterPlacement)
        {
            var location = player.PickTileLocation(tile, _showTilePoints);
            var board = _playerAndBoardCollection[player];
            while (!board.LocationIsPresentOnBoard(location) || !board.LocationIsEmpty(location))
            {
                player.SendMessage(new InformationMessage($"The placement of {char.ToUpper(tile.Letter)} is invalid, please place it somewhere else."));
                location = player.PickTileLocation(tile, _showTilePoints);
            }

            board.InsertTileAt(location, tile);

            if (displayBoardAfterPlacement)
            {
                DisplayBoardForPlayer(player);
            }

            if (_playerAndBoardCollection.Players.Count > 1)
            {
                player.SendMessage(new InformationMessage("Waiting for opponents..."));
            }
        }

        /// <summary>
        /// Asynchronous let the specified player place a tile on his board.
        /// </summary>
        /// <param name="player">Player to let pick a location for the specifed tile.</param>
        /// <param name="tile">Tile to be placed on player's board.</param>
        /// <param name="displayBoardAfterPlacement">Indicates whether to display the player's board after a tile placement</param>
        protected Task LetPlayerPlaceTileOnBoardAsync(PlayerBase player, Tile tile, bool displayBoardAfterPlacement)
        {
            return Task.Run(() => LetPlayerPlaceTileOnBoard(player, tile, displayBoardAfterPlacement));
        }

        /// <summary>
        /// Generate a leaderboard from a list of scores, where the player with the
        /// highest score is on first place.
        /// </summary>
        /// <param name="scores">A list of key-value pairs where the key is a player and the value is the player's score.</param>
        /// <returns>The generated leaderboard</returns>
        protected virtual string GenerateLeaderboard(List<KeyValuePair<PlayerBase, int>> scores)
        {
            var winners = scores.OrderByDescending((s) => s.Value).ToArray();
            var leaderboard = new StringBuilder();
            leaderboard.AppendLine("  Leaderboard  ");
            for (int i = 0; i < winners.Length; i++)
            {
                leaderboard.AppendLine($"{i + 1}. {winners[i].Key.ID} ({winners[i].Value} points)");
            }

            return leaderboard.ToString();
        }

        /// <summary>
        /// Send an information message to all players.
        /// </summary>
        /// <param name="message">Message to send</param>
        protected void SendInformationToAllPlayers(string message)
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                var leaderBoardMessage = new InformationMessage(message);
                player.SendMessage(leaderBoardMessage);
            }
        }

        /// <summary>
        /// Send a <see cref="GameHasEndedMessage"/> to all players, except the any <see cref="LocalPlayer"/>.
        /// </summary>
        protected void TellAllPlayersGameHasEnded()
        {
            foreach (var player in _playerAndBoardCollection.Players)
            {
                if (player is LocalPlayer) continue;

                player.SendMessage(new GameHasEndedMessage());
            }
        }

        /// <summary>
        /// Disconnect all players and stop <see cref="_netHost"/>.
        /// </summary>
        protected virtual void StopNetworkHost()
        {
            _netHost.DisconnectAllClients();
            _netHost.Stop();
        }

        /// <summary>
        /// Used to setup the game before running the main <see cref="GameLoop"/>.
        /// </summary>
        protected abstract void Setup();

        /// <summary>
        /// Used to run the game mode.
        /// </summary>
        protected abstract void GameLoop();

        /// <summary>
        /// Used to cleanup and release resources when the game has ended.
        /// </summary>
        protected abstract void CleanUp();

        /// <summary>
        /// Used to generate a board.
        /// </summary>
        /// <returns>A generated board</returns>
        protected abstract StandardBoard BuildBoard();
    }
}
