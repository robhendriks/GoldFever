using System;

namespace GoldFever.Core
{
    public class GameOverException : GameException
    {
        public GameOverException()
            : base()
        {

        }

        public GameOverException(string message)
            : base(message)
        {

        }

        public GameOverException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
