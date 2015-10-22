using GoldFever.Core.Cart;
using GoldFever.Core.Generic;
using GoldFever.Core.Level;
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

        public virtual BaseTrack Next
        {
            get { return _next; }
        }

        protected BaseCart _cart;

        public BaseCart Cart
        {
            get { return _cart; }
            set
            {
                if (_cart == null && value != null)
                    OnEnter();
                else if (_cart != null && value == null)
                    OnLeave();
                else
                    throw new InvalidOperationException("Track already occupied.");

                _cart = value;
            }
        }

        public bool Occupied
        {
            get { return (_cart != null); }
        }

        public BaseTrack(Vector position, Direction direction)
        {
            _position = position;
            _direction = direction;
        }

        public virtual bool Link(BaseLevel level, out BaseTrack[] results)
        {
            if(_direction == Direction.None)
            {
                results = null;
                return false;
            }

            results = level.GetTracksFacing(_position, _direction);

            bool valid = (results.Length != 0);
            if (valid)
                _next = results[0];

            return valid;
        }

        protected virtual void OnEnter()
        {
            
        }

        protected virtual void OnLeave()
        {
            
        }

        public virtual bool CanEnter(BaseCart cart)
        {
            return !Occupied;
        }

        public virtual bool CanLeave()
        {
            return (_next != null ? !_next.Occupied : true);
        }
    }
}
