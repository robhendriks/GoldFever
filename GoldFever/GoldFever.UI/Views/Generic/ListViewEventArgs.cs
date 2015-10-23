using System;

namespace GoldFever.UI.Views.Generic
{
    public class ListViewEventArgs<K, V> : EventArgs
    {
        #region Properties

        public ListViewItem<K, V> Item { get; set; }

        #endregion


        #region Constructors

        public ListViewEventArgs(ListViewItem<K, V> item)
        {
            Item = item;
        }

        public ListViewEventArgs()
        {

        }

        #endregion
    }
}
