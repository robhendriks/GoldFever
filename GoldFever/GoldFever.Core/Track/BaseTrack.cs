using GoldFever.Core.Generic;
using System;

namespace GoldFever.Core.Track
{
    public abstract class BaseTrack
    {
        protected Vector _position;

        public Vector Position
        {
            get { return _position; }
        }

        protected Direction _direction;

        public Direction Direction
        {
            get { return _direction; }
        }

        protected BaseTrack _next;

        public BaseTrack Next
        {
            get { return _next; }
        }

        public BaseTrack(Vector position, Direction direction)
        {
            _position = position;
            _direction = direction;
        }

        public virtual bool CanEnter()
        {
            return true;
        }

        public virtual bool CanLeave()
        {
            return true;
        }
    }
}
