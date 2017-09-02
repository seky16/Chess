// ReSharper disable StyleCop.SA1600

namespace Chess
{
    using System;

    [Serializable]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
            : this("Invalid Move")
        {
        }

        public InvalidMoveException(string message)
            : base(message)
        {
        }

        public InvalidMoveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
