using GoldFever.Core.Track;
using System;

namespace GoldFever.Core.Cart
{
    public sealed class BaseCart
    {
        #region Private Fields

        private bool firstTick;

        #endregion


        #region Properties

        private BaseTrack _current;

        public BaseTrack Current
        {
            get { return _current; }
            set { _current = value; }
        }

        private bool _disposed;

        public bool IsDisposed
        {
            get { return _disposed; }
        }

        private bool _empty;

        public bool IsEmpty
        {
            get { return _empty; }
        }

        #endregion


        #region Constructors

        public BaseCart()
        {
            firstTick = true;
            _disposed = _empty = false;
        }

        #endregion


        #region Methods

        private void Move()
        {
            if (_disposed)
                return;
            else if(firstTick)
            {
                firstTick = false;
                return;
            }

            var cur = Current;
            var next = Current?.Next;

            if (cur == null)
            {
                _disposed = true;
                return;
            }
            else if(next != null && !next.CanEnter(this))
            {
                // Quick n' dirty
                if (next is SwitchTrack)
                    return;

                if (!next.Collides() && cur.Collides())
                    throw new GameOverException();
                else if (next.Collides())
                    throw new GameOverException();
            }
            else if (next != null && next.CanEnter(this))
            {
                _current.OnLeave();

                _current = next;
                _current.OnEnter(this);
            }
        }

        public void Update()
        {
            Move();
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public void Empty()
        {
            _empty = true;
        }

        public byte Char()
        {
            return (byte)(_empty ? 1 : 2);
        }

        #endregion
    }
}
