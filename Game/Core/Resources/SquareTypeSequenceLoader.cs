using Game.Core.Board;
using Game.Core.Exceptions;
using System;
using System.IO;
using System.Linq;

namespace Game.Core.Resources
{
    /// <summary>
    /// Load user defined board layouts.
    /// </summary>
    public class SquareTypeSequenceLoader : IResourceLoader<SquareType[,]>
    {
        public SquareType[,] LoadFromFile(string filepath)
        {
            if (!Path.IsPathRooted(filepath))
            {
                filepath = Path.Combine(Settings.AssemblyDirectory, filepath);
            }
            string[] linesInFile = File.ReadAllLines(filepath);
            string firstLineInFile = linesInFile.FirstOrDefault() ?? throw new SquareSequenceFormatException("File is empty");
            var sequenceSize = GetSquareSequenceSize(firstLineInFile);

            string[] rawSquareRows = linesInFile[1..].ToArray();
            var squareSequenceResult = new SquareType[sequenceSize.rows, sequenceSize.columns];

            try
            {
                for (int row = 0; row < sequenceSize.rows; row++)
                {
                    var rowSequence = rawSquareRows[row].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    for (int col = 0; col < sequenceSize.columns; col++)
                    {
                        squareSequenceResult[row, col] = GetSquareType(rowSequence[col]);
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new SquareSequenceFormatException($"The specified board size on line 1 did not match the actual lines in file \"{filepath}\"");
            }
            catch
            {
                throw;
            }
            return squareSequenceResult;
        }

        private (int rows, int columns) GetSquareSequenceSize(string str)
        {
            var parts = str.Split(new char[]{ 'x', 'X' }, 2);

            if (!int.TryParse(parts[0], out int rows) ||
               !int.TryParse(parts[1], out int columns))
            {
                throw new SquareSequenceFormatException($"Missing size of board. Expected <rows>x<columns>, got \"{str}\"");
            }

            return (rows, columns);
        }

        private SquareType GetSquareType(string str)
        {
            switch (str.ToUpper())
            {
                case "R":
                    return SquareType.Regular;
                case "DL":
                    return SquareType.DoubleLetter;
                case "TL":
                    return SquareType.TripleLetter;
                case "DW":
                    return SquareType.DoubleWord;
                case "TW":
                    return SquareType.TrippleWord;
                default:
                    throw new SquareSequenceFormatException($"Unknown square type \"{str}\"");
            }
        }
    }
}
