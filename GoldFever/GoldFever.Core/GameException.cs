using System;

namespace GoldFever.Core
{
    public class GameException : Exception
    {
        #region Constructors

        public GameException()
            : base()
        {

        }

        public GameException(string message)
            : base(message)
        {

        }

        public GameException(string message, Exception inner)
            : base(message, inner)
        {

        }

        #endregion
    }
}
