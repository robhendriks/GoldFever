using System;

namespace GoldFever.UI.Views
{
    public sealed class ViewManager
    {
        private static ViewManager instance;

        #region Properties

        private bool _active;

        public bool Active
        {
            get { return _active; }
        }

        private View _previous;

        public View Previous
        {
            get { return _previous; }
        }

        private View _current;

        public View Current
        {
            get { return _current; }
            set
            {
                _previous = _current;
                _previous?.Leave(this);

                _current = value;
                _current?.Enter(this);

                OnCurrentChanged();
            }
        }

        #endregion


        #region Constructors

        private ViewManager()
        {
            _active = false;
        }

        #endregion


        #region Methods

        private void Draw()
        {
            Console.ResetColor();
            Console.Clear();

            Current?.Draw();
        }

        private void Update(ConsoleKey key)
        {
            // TODO: Handle default keys

            Current?.Update(key);
        }

        private void Loop()
        {
            if (_active)
                return;

            Console.CursorVisible = false;

            _active = true;
            Draw();

            while (_active)
            {
                if (_current == null)
                    break;

                var info = Console.ReadKey();

                Draw();
                Update(info.Key);
            }

            _active = false;
            Console.Clear();

            OnClosed();
        }

        public void Invalidate()
        {
            Draw();
        }

        public void Back(bool remember = true)
        {
            if (Current == null)
                return;

            if (Current.CanLeave() && Previous != null)
            {
                Current = Previous;
                Invalidate();
            }
            else if (Current.CanLeave() && Previous == null)
                Current = null;

            if (!remember)
                _previous = null;
        }

        public void Close()
        {
            if (!_active)
                return;

            _active = false;
        }

        #endregion


        #region Events

        private void OnCurrentChanged()
        {
            if (CurrentChanged != null)
                CurrentChanged();

            Loop();
        }

        private void OnClosed()
        {
            if (Closed != null)
                Closed();
        }

        public event CurrentChangedHandler CurrentChanged;
        public event ClosedHandler Closed;

        public delegate void CurrentChangedHandler();
        public delegate void ClosedHandler();

        #endregion


        #region Static methods

        public static ViewManager GetInstance()
        {
            return instance ?? (instance = new ViewManager());
        }

        #endregion
    }
}
