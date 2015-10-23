using System;

namespace GoldFever.UI.Views.Generic
{
    public class DefaultListView : ListView<int, string>
    {
        #region Constructors

        public DefaultListView(string title, string text)
            : base(title, text)
        {

        }

        #endregion
    }
}
