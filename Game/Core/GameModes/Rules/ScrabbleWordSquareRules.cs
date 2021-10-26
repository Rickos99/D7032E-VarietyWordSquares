using Game.Core.Board;
using Game.Core.Language;
using System;
using System.Collections.Generic;

namespace Game.Core.GameModes.Rules
{
    public class ScrabbleWordSquareRules : IGameRules
    {
        private readonly Dictionary _dictionary;

        public ScrabbleWordSquareRules(Dictionary dictionary)
        {
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        public bool BoardHasReachedGameOver(StandardBoard board)
        {
            return board.IsFilled();
        }

        public int CalculateScore(StandardBoard board)
        {
            int score = 0;
            var squareSequences = board.GetAllSquareSequences();
            var words = GetAllWords(squareSequences);
            foreach (var word in words)
            {
                score += CalculateWordScore(word);
            }
            return score;
        }

        private int CalculateWordScore(List<Square> word)
        {
            var wordMultiplier = 1;
            var wordScore = 0;
            foreach (var square in word)
            {
                var tile = square.Tile;
                var letterMultiplier = 1;

                switch (square.SquareType)
                {
                    case SquareType.Regular:
                        letterMultiplier = 1;
                        break;
                    case SquareType.DoubleLetter:
                        letterMultiplier = 2;
                        break;
                    case SquareType.TripleLetter:
                        letterMultiplier = 3;
                        break;
                    case SquareType.DoubleWord:
                        wordMultiplier *= 2;
                        break;
                    case SquareType.TrippleWord:
                        wordMultiplier *= 3;
                        break;
                }

                wordScore += tile.Points * letterMultiplier;
            }

            return wordScore * wordMultiplier;
        }

        private List<List<Square>> GetAllWords(IEnumerable<List<Square>> squareSequences)
        {
            var words = new List<List<Square>>();
            foreach (var sequence in squareSequences)
            {
                if (!_dictionary.ContainsSquareSequence(sequence)) continue;

                words.Add(sequence);
            }
            return words;
        }
    }
}
