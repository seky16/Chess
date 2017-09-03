// ReSharper disable StyleCop.SA1600
// ReSharper disable StyleCop.SA1402

namespace Chess
{
    using System;

    [Serializable]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message = "Invalid Move")
            : base(message)
        {
        }

        public InvalidMoveException(string message, Exception innerException)
            : base(message, innerException)
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

        public InvalidFenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
