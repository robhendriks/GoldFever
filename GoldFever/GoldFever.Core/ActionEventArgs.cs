using System;

namespace GoldFever.Core
{
    public class ActionEventArgs : EventArgs
    {
        #region Properties

        public Action Action { get; set; }

        #endregion


        #region Constructors

        public ActionEventArgs()
            : this(Action.None)
        {

        }

        public ActionEventArgs(Action action)
        {
            Action = action;
        }

        #endregion
    }
}
