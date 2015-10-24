using System;

using Color = System.ConsoleColor;

namespace GoldFever.UI.Views
{
    public class View : IView
    {
        #region Properties

        public ViewBehaviour Behaviour { get; set; }

        protected string _title;

        public string Title
        {
            get { return _title ?? "null"; }
            set
            {
                _title = value;
                OnTitleChanged();
            }
        }

        protected string _text;

        public string Text
        {
            get { return _text ?? "null"; }
            set
            {
                _text = value;
                OnTextChanged();
            }
        }

        #endregion


        #region Constructors

        public View(string title = null, string text = null)
        {
            Behaviour = ViewBehaviour.Default;

            _title = title;
            _text = text;

            Initialize();
        }

        #endregion


        #region Methods

        private void Initialize()
        {
            TextChanged += Screen_TextChanged;
            TitleChanged += Screen_TitleChanged;
        }

        private void Screen_TextChanged(object sender)
        {
            Invalidate();
        }

        private void Screen_TitleChanged(object sender)
        {
            Console.Title = _title;
            Invalidate();
        }

        public void Invalidate()
        {
            ViewManager.GetInstance().Invalidate();
        }

        protected void DrawTitle()
        {
            Console.Write("\n\n");
            WriteLine(_title, true);
        }

        protected void DrawText()
        {
            WriteLine(_text);
        }

        public virtual void Draw()
        {
            DrawTitle();
            DrawText();
        }

        public virtual void Update(ConsoleKey key)
        {
            var manager = ViewManager.GetInstance();

            if (Behaviour == ViewBehaviour.Default)
            {
                switch (key)
                {
                    case ConsoleKey.Escape | ConsoleKey.Backspace:
                        manager.Back(); break;
                }
            }
        }

        public virtual bool CanLeave()
        {
            return true;
        }

        public void Show()
        {
            ViewManager.GetInstance().Current = this;
        }

        public void Hide()
        {
            var manager = ViewManager.GetInstance();

            if (manager.Current.Equals(this))
                manager.Current = null;
        }

        public void Enter(object sender)
        {
            if (!(sender is ViewManager))
                throw new InvalidOperationException("This method can only be called by the view manager.");

            OnEntered();
        }

        public void Leave(object sender)
        {
            if (!(sender is ViewManager))
                throw new InvalidOperationException("This method can only be called by the view manager.");

            OnLeft();
        }

        #endregion


        #region Static methods

        public static void WriteLine(string input, bool inverted = false, int padding = -48)
        {
            WriteLine(input,
                (inverted ? Color.Black : Color.Gray),
                (inverted ? Color.Gray : Color.Black),
                padding);
        }

        public static void WriteLine(string input, Color foreground, Color background, int padding = -48)
        {
            string[] lines = input.Split('\n');
            string format = (padding < 0 ? $"{{0,{padding}}}" : "{0}");

            foreach (string line in lines)
            {
                Console.ResetColor();
                Console.Write("".PadLeft(4, ' '));

                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
                Console.WriteLine(string.Format(format, line));
            }
        }

        #endregion


        #region Events

        protected void OnTitleChanged()
        {
            if (TitleChanged != null)
                TitleChanged(this);
        }

        protected void OnTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this);
        }

        protected void OnEntered()
        {
            if (Entered != null)
                Entered(this);

            Console.Title = _title;
        }

        protected void OnLeft()
        {
            if (Left != null)
                Left(this);
        }

        public event TitleChangedEventHandler TitleChanged;
        public event TextChangedEventHandler TextChanged;
        public event EnteredEventHandler Entered;
        public event EnteredEventHandler Left;

        public delegate void TitleChangedEventHandler(object sender);
        public delegate void TextChangedEventHandler(object sender);
        public delegate void EnteredEventHandler(object sender);
        public delegate void LeftEventHandler(object sender);

        #endregion
    }
}
