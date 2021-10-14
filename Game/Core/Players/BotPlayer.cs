﻿using Game.Core.Board;
using Game.Core.Communication;
using Game.Core.Exceptions;
using System;
using System.Linq;

namespace Game.Core.Players
{
    class BotPlayer : PlayerBase
    {
        private readonly Random _rng;
        private readonly BoardBase _board;

        public static Type[] SupportedQuestions
        {
            get => new Type[] {
                typeof(PickALetterQuestion),
                typeof(PickLetterLocationQuestion)
            };
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="BotPlayer"/> class.
        /// </summary>
        /// <param name="board">Board in which the bot will have as a decision base </param>
        /// <exception cref="ArgumentNullException"></exception>
        public BotPlayer(BoardBase board)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            _rng = new Random();
            _board = board;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="BotPlayer"/> class with a 
        /// determanistic behevior.
        /// </summary>
        /// <remarks>
        /// Behavior of the bot is only determanistic if the <paramref name="board"/> 
        /// and <paramref name="rngSeed"/> is kept constant and 
        /// </remarks>
        /// <param name="board">Board in which the bot will have as a decision base </param>
        /// <param name="rngSeed">Seed to use in decisions</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BotPlayer(BoardBase board, int rngSeed)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            _rng = new Random(rngSeed);
            _board = board;
        }

        public override string AskQuestion(IQuestion question)
        {
            if (question is PickALetterQuestion) return AnswerPickLetter().ToString();
            if (question is PickLetterLocationQuestion) return AnswerPickLetterLocation();

            throw new UnsupportedQuestionException(question.GetType());
        }

        public override void SendMessage(IMessage message) { }

        /// <summary>
        /// Check if a <see cref="BotPlayer"/> can answer a certain type of question
        /// </summary>
        /// <param name="question">Question to check</param>
        /// <returns><c>true</c> if the <paramref name="question"/> can be answered; Otherwise <c>false</c></returns>
        public static bool BotCanAnswerQuestion(IQuestion question)
        {
            return SupportedQuestions.Contains(question.GetType());
        }

        private char AnswerPickLetter()
        {
            var avaliableLetters = _board.AvaliableLetters;
            var letterIndex = _rng.Next(avaliableLetters.Length - 1);
            return avaliableLetters[letterIndex].Letter;
        }

        private string AnswerPickLetterLocation()
        {
            var emptyLocations = _board.GetAllEmptyLocations();
            var locationIndex = _rng.Next(emptyLocations.Length - 1);
            return emptyLocations[locationIndex];
        }
    }
}
