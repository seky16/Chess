// ReSharper disable StyleCop.SA1600
// ReSharper disable StyleCop.SA1402

namespace Chess
{
    using System;

    [Serializable]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message = "Invalid move")
            : base(message)
        {
        }
    }

    [Serializable]
    public class InvalidCoordsException : Exception
    {
        public InvalidCoordsException(string message = "Invalid coordinates!")
            : base(message)
        {
        }
    }

    [Serializable]
    public class InvalidFenException : Exception
    {
        public InvalidFenException(string message = "You entered invalid FEN notation!")
            : base(message)
        {
        }

        public InvalidFenException(int position)
            : base($"You entered invalid FEN notation at position {position}!")
        {
        }
    }
}
