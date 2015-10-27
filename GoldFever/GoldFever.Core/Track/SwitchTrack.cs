using GoldFever.Core.Generic;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever.Core.Track
{
    public abstract class SwitchTrack : BaseTrack
    {
        #region Constants

        protected const byte CharUp = 24,
                             CharDown = 25;

        #endregion


        #region Properties

        protected ConsoleKey _key;

        public ConsoleKey Key
        {
            get { return _key; }
        }

        protected SwitchMode _mode;

        public SwitchMode Mode
        {
            get { return _mode; }
        }

        public int SelectedIndex
        {
            get
            {
                switch (_mode)
                {
                    default: return -1;
                    case SwitchMode.Up: return 0;
                    case SwitchMode.Down: return 1;
                }
            }
        }

        #endregion


        #region Constructors

        public SwitchTrack(Vector position, Direction direction, ConsoleKey key)
            : base(position, direction)
        {
            _key = key;
            _mode = SwitchMode.Down;
        }

        #endregion


        #region Methods

        public void Toggle()
        {
            if (_cart != null)
                return;

            _mode = (_mode == SwitchMode.Up
                ? SwitchMode.Down
                : SwitchMode.Up);
        }

        public SwitchMode Compare(Vector target)
        {
            if (target.Y < Position.Y)
                return SwitchMode.Up;
            else if (target.Y > Position.Y)
                return SwitchMode.Down;
            else
                throw new ArgumentException("Invalid target {target}.");
        }

        public override short Attributes()
        {
            return Color.ForegroundRed | Color.BackgroundDarkRed;
        }

        #endregion
    }
}
