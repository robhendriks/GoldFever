using System;
using System.Collections.Generic;

namespace GoldFever.UI.Views.Generic
{
    public class ListView<K, V> : View
    {
        private const int Height = 8;

        #region Properties

        protected List<ListViewItem<K, V>> _items;

        public List<ListViewItem<K, V>> Items
        {
            get { return _items; }
            set
            {
                if (value == null)
                    return;

                _items = value;
                OnItemsChanged();
            }
        }

        protected int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                var tmp = _selectedIndex;

                if (value < 0)
                    _selectedIndex = 0;
                else if (value > _items.Count - 1)
                    _selectedIndex = _items.Count - 1;
                else
                    _selectedIndex = value;

                if (_selectedIndex != tmp)
                    OnSelectedIndexChanged();
            }
        }

        public ListViewItem<K, V> SelectedItem
        {
            get { return (_items.Count > 0 ? _items[_selectedIndex] : null); }
        }

        #endregion

        #region Constructors

        public ListView(string title, string text)
            : base(title, text)
        {
            _items = new List<ListViewItem<K, V>>();
        }

        #endregion


        #region Methods

        protected virtual void DrawItems()
        {
            Console.Write("\n");

            int start = (_selectedIndex > Height - 1 ? _selectedIndex - (Height - 1) : 0),
                end = start + Height;

            if (end > _items.Count)
                end = _items.Count;

            for(int i = start; i < end; i++)
                WriteLine(_items[i].ToString(), (i == _selectedIndex), 0);

            //int length = (_items.Count > Height ? Height : _items.Count),
            //    start = (_selectedIndex > length ? _selectedIndex : 0);

            //for(int i = start; i < start + length -1; i++)
            //    WriteLine(_items[i].ToString(), (i == _selectedIndex), 0);
        }

        public override void Draw()
        {
            base.Draw();
            DrawItems();
        }

        public override void Update(ConsoleKey key)
        {
            base.Update(key);

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    SelectedIndex--; break;
                case ConsoleKey.DownArrow:
                    SelectedIndex++; break;
                case ConsoleKey.Enter:
                    OnSelected(); break;
            }
        }

        public override bool CanLeave()
        {
            return false;
        }

        #endregion


        #region Events

        protected void OnItemsChanged()
        {
            if (ItemsChanged != null)
                ItemsChanged(this);

            Invalidate();
        }

        protected void OnSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, new ListViewEventArgs<K, V>(SelectedItem));

            Invalidate();
        }

        protected void OnSelected()
        {
            if (Selected != null)
                Selected(this, new ListViewEventArgs<K, V>(SelectedItem));

            Invalidate();
        }

        public event ItemsChangedHandler ItemsChanged;
        public event SelectedIndexChangedHandler SelectedIndexChanged;
        public event SelectedHandler Selected;

        public delegate void ItemsChangedHandler(object sender);
        public delegate void SelectedIndexChangedHandler(object sender, ListViewEventArgs<K, V> e);
        public delegate void SelectedHandler(object sender, ListViewEventArgs<K, V> e);

        #endregion
    }
}
