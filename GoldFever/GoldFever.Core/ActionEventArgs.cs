using System;

namespace GoldFever.Core
{
    public class ActionEventArgs : EventArgs
    {
        public Action Action { get; set; }

        public ActionEventArgs()
            : this(Action.None)
        {

        }

        public ActionEventArgs(Action action)
        {
            Action = action;
        }
    }
}
