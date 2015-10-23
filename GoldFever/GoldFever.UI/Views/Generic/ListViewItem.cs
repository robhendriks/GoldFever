using System;

namespace GoldFever.UI.Views.Generic
{
    public class ListViewItem<K, V>
    {
        #region Properties

        public K Key { get; set; }
        public V Value { get; set; }

        #endregion


        #region Constructors

        public ListViewItem(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public ListViewItem()
        {

        }

        #endregion


        #region Methods

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion
    }
}
