using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    //[Serializable]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
        {
            //InvalidMoveException("Invalid Move");
        }

        public InvalidMoveException(string message)
            : base(message)
        { }

        public InvalidMoveException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
