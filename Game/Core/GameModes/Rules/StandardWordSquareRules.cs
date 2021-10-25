using Game.Core.Board;
using Game.Core.Language;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.GameModes.Rules
{
    public class StandardWordSquareRules : IGameRules
    {
        private readonly Dictionary _dictionary;

        public StandardWordSquareRules(Dictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public bool BoardHasReachedGameOver(StandardBoard board)
        {
            return board.IsFilled();
        }

        public int CalculateScore(StandardBoard board)
        {
            int score = 0;
            HashSet<string> uniqueWords = GetUniqueWordSquareSequences(board);
            foreach (var uniqueWord in uniqueWords)
            {
                if (uniqueWord.Length < 3) continue;
                if (uniqueWord.Length == 3)
                {
                    score += 1;
                }
                else if (uniqueWord.Length > 3)
                {
                    score += (uniqueWord.Length - 3) * 2;
                }
            }
            return score;
        }

        private HashSet<string> GetUniqueWordSquareSequences(StandardBoard board)
        {
            var squareSequences = board.GetAllSquareSequences();
            var words = new List<List<Square>>();

            foreach (var squareSequence in squareSequences)
            {
                if (!_dictionary.ContainsSquareSequence(squareSequence)) continue;

                words.Add(squareSequence);
            }

            var uniqueSubSequences = words.Select((sequence) => new string(sequence.Select((square) => square?.Tile.Letter ?? '\0').ToArray()));
            var uniqueWords = new HashSet<string>();
            foreach (var uniqueSubSequence in uniqueSubSequences)
            {
                if (uniqueWords.Contains(uniqueSubSequence)) continue;
                uniqueWords.Add(uniqueSubSequence);
            }

            return uniqueWords;
        }
    }
}
