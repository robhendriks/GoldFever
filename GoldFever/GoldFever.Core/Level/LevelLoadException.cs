using System;

namespace GoldFever.Core.Level
{
    public class LevelLoadException : GameException
    {
        #region Constructors

        public LevelLoadException()
            : base()
        {

        }

        public LevelLoadException(string message)
            : base(message)
        {

        }

        public LevelLoadException(string message, Exception inner)
            : base(message, inner)
        {

        }

        #endregion
    }
}
