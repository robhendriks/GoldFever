using GoldFever.Core.Generic;
using System;

namespace GoldFever.Core.Track
{
    public abstract class SwitchTrack : BaseTrack
    {
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
                switch(_mode)
                {
                    default: return -1;
                    case SwitchMode.Up: return 0;
                    case SwitchMode.Down: return 1;
                }
            }
        }

        public SwitchTrack(Vector position, Direction direction, ConsoleKey key)
            : base(position, direction)
        {
            _key = key;
            _mode = SwitchMode.Down;
        }

        public void Toggle()
        {
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
    }
}
