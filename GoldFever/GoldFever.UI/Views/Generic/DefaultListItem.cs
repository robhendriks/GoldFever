using System;

namespace GoldFever.UI.Views.Generic
{
    public class DefaultListViewItem : ListViewItem<int, string>
    {
        #region Constructors

        public DefaultListViewItem(int key, string value)
            : base(key, value)
        {

        }

        public DefaultListViewItem()
        {

        }

        #endregion
    }
}
