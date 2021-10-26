using Game.Core.Board;

namespace Game.Tests.Core.Board.Boards
{
    //
    // CAUTION! This class is used is many unit tests. Changing below fields may result in unexpected behavior.
    //
    static class PredefinedBoardLayouts
    {
        public static Square[,] Squares_AllRegular_3x3_Filled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('B', 3)), new(SquareType.Regular, new('D', 2))},
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('E', 1))},
                    { new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1))},
                };
            }
        }

        public static Square[,] Squares_AllRegular_4x4_Filled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular, new('T', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('D', 2)), new(SquareType.Regular, new('T', 1))},
                    { new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('E', 1))},
                    { new(SquareType.Regular, new('S', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('S', 1))},
                    { new(SquareType.Regular, new('T', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('S', 1)), new(SquareType.Regular, new('T', 1))},
                };
            }
        }

        public static Square[,] Squares_MixedSquareTypes_4x4_Filled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.DoubleLetter, new('T', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('D', 2)), new(SquareType.TripleLetter, new('T', 1))},
                    { new(SquareType.Regular, new('E', 1)), new(SquareType.DoubleWord, new('A', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.DoubleWord, new('E', 1))},
                    { new(SquareType.Regular, new('S', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.Regular, new('A', 1)), new(SquareType.TrippleWord, new('S', 1))},
                    { new(SquareType.Regular, new('T', 1)), new(SquareType.Regular, new('E', 1)), new(SquareType.Regular, new('S', 1)), new(SquareType.Regular, new('T', 1))},
                };
            }
        }

        public static Square[,] Squares_AllRegular_2x2_SemiFilled
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular), new(SquareType.Regular, new('A', 1))},
                    { new(SquareType.Regular), new(SquareType.Regular)},
                };
            }
        }

        public static Square[,] Squares_AllRegular_2x2_Empty
        {
            get
            {
                return new Square[,] {
                    { new(SquareType.Regular), new(SquareType.Regular)},
                    { new(SquareType.Regular), new(SquareType.Regular)},
                };
            }
        }
    }
}
