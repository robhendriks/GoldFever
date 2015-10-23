using System;

namespace GoldFever.UI.Views.Generic
{
    public class OptionListView : ListView<bool, string>
    {
        #region Constructors

        public OptionListView(string title, string text)
            : base(title, text)
        {
            
        }

        public override void Update(ConsoleKey key)
        {
            base.Update(key);

            switch(key)
            {
                case ConsoleKey.Spacebar:
                    Select(); OnSelected(); break;
            }
        }

        #endregion


        #region Methods

        protected void Select()
        {
            if (SelectedItem == null)
                return;

            SelectedItem.Key = !SelectedItem.Key;
        }

        #endregion
    }
}
