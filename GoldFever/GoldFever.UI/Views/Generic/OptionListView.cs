using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldFever.UI.Views.Generic
{
    public class OptionListView : ListView<bool, string>
    {
        #region Properties

        SelectionMode Mode { get; set; }

        #endregion


        #region Constructors

        public OptionListViewItem[] SelectedItems
        {
            get { return GetSelectedItems().ToArray(); }
        }

        public OptionListView(string title, string text)
            : base(title, text)
        {
            Mode = SelectionMode.Multiple;
        }

        public override void Update(ConsoleKeyInfo info)
        {
            base.Update(info);

            switch(info.Key)
            {
                case ConsoleKey.Spacebar:
                    Select(); OnSelected(); break;
            }
        }

        #endregion


        #region Methods

        protected IEnumerable<OptionListViewItem> GetSelectedItems()
        {
            foreach (var item in _items)
                if (item.Key)
                    yield return (OptionListViewItem)item;
        }

        private void Select()
        {
            if (SelectedItem == null)
                return;

            SelectedItem.Key = !SelectedItem.Key;

            if (SelectedItem.Key && Mode == SelectionMode.Single)
                DeselectAll();

            OnItemStateChanged();
        }

        private void DeselectAll()
        {
            foreach (var item in _items)
                if (!item.Equals(SelectedItem))
                    item.Key = false;
        }

        #endregion


        #region Events

        protected void OnItemStateChanged()
        {
            if (ItemStateChanged != null)
                ItemStateChanged(this, new ListViewEventArgs<bool, string>(SelectedItem));

            Invalidate();
        }

        public event ItemStateChangedHandler ItemStateChanged;
        public delegate void ItemStateChangedHandler(object sender, ListViewEventArgs<bool, string> e);

        #endregion
    }
}
