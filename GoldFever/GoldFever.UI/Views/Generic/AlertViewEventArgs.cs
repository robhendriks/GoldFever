using System;

namespace GoldFever.UI.Views.Generic
{
    public class AlertViewEventArgs : EventArgs
    {
        public AlertViewResult Result { get; set; }

        public AlertViewEventArgs(AlertViewResult result = AlertViewResult.None)
        {
            Result = result;
        }

        public AlertViewEventArgs()
        {

        }
    }
}
