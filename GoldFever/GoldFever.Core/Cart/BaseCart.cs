using GoldFever.Core.Track;
using System;

namespace GoldFever.Core.Cart
{
    // test
    public sealed class BaseCart
    {
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

        public BaseCart()
        {
            _disposed = _empty = false;
        }

        private void Move()
        {
            if (_disposed)
                return;

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
    }
}
