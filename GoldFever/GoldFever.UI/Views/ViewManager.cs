﻿using System;

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
        }

        public void Invalidate()
        {
            Draw();
        }

        public void Back()
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
        }

        #endregion


        #region Events

        private void OnCurrentChanged()
        {
            if (CurrentChanged != null)
                CurrentChanged();

            Loop();
        }

        public event CurrentChangedHandler CurrentChanged;
        public delegate void CurrentChangedHandler();

        #endregion


        #region Static methods

        public static ViewManager GetInstance()
        {
            return instance ?? (instance = new ViewManager());
        }

        #endregion
    }
}
