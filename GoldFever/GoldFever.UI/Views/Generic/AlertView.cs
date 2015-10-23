using System;

using Result = GoldFever.UI.Views.Generic.AlertViewResult;
using Buttons = GoldFever.UI.Views.Generic.AlertViewButtons;

namespace GoldFever.UI.Views.Generic
{
    public class AlertView : View
    {
        #region Members

        private Result[] results;

        #endregion


        #region Properties

        private Buttons _buttons;

        public Buttons Buttons
        {
            get { return _buttons; }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                var tmp = _selectedIndex;

                if (value < 0)
                    _selectedIndex = 0;
                else if (value > results.Length - 1)
                    _selectedIndex = results.Length - 1;
                else
                    _selectedIndex = value;

                if(_selectedIndex != tmp)
                    OnSelectedIndexChanged();
            }
        }

        public Result SelectedResult
        {
            get { return results[_selectedIndex]; }
        }

        #endregion

        #region Constructors

        public AlertView(string title, string text, Buttons buttons = Buttons.OK)
            : base(title, text)
        {
            _buttons = buttons;
            _selectedIndex = 0;

            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            switch(_buttons)
            {
                case Buttons.OK:
                    results = new Result[] { Result.OK }; break;
                case Buttons.OKCancel:
                    results = new Result[] { Result.OK, Result.Cancel }; break;
                case Buttons.YesNo:
                    results = new Result[] { Result.Yes, Result.No }; break;
                case Buttons.YesNoCancel:
                    results = new Result[] { Result.Yes, Result.No, Result.Cancel }; break;
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawButtons();
        }

        protected void DrawButtons()
        {
            if (results == null)
                return;

            Console.Write("\n");
            var line = "";

            for(int i = 0; i < results.Length; i++)
            {
                var result = results[i];
                var selected = (i == _selectedIndex);

                line += (selected ? $"[{result}] " : $" {result}  ");
            }

            WriteLine(line);
        }

        public override void Update(ConsoleKey key)
        {
            base.Update(key);

            switch(key)
            {
                case ConsoleKey.LeftArrow:
                    SelectedIndex--; break;
                case ConsoleKey.RightArrow:
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

        protected void OnSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this);

            Invalidate();
        }

        protected void OnSelected()
        {
            if (Selected != null)
                Selected(this, new AlertViewEventArgs(SelectedResult));

            Invalidate();
        }

        public event SelectedIndexChangedHandler SelectedIndexChanged;
        public event SelectedHandler Selected;

        public delegate void SelectedIndexChangedHandler(object sender);
        public delegate void SelectedHandler(object sender, AlertViewEventArgs e);

        #endregion
    }
}
