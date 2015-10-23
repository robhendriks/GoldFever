using System;

namespace GoldFever.UI.Views.Generic
{
    public class OptionListViewItem : ListViewItem<bool, string>
    {
        #region Constructors

        public OptionListViewItem(bool key, string value)
            : base(key, value)
        {

        }

        public OptionListViewItem(string value)
            : base(false, value)
        {

        }

        #endregion


        #region Methods

        public override string ToString()
        {
            return string.Format("[{0}] {1}", (Key ? 'X' : ' '), Value);
        }

        #endregion
    }
}
