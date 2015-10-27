using System;

namespace GoldFever.Core.Ship
{
    public class BaseShip
    {
        #region Properties

        public const int Capacity = 8;
        public const int Width = 3;

        private ShipPort _port;

        public ShipPort Port
        {
            get { return _port; }
        }

        private int _index;

        public int Index
        {
            get { return _index; }
        }

        private int _size;

        public int Size
        {
            get { return _size; }
            set
            {
                if (value < 0)
                    _size = 0;
                if (value > Capacity)
                    _size = Capacity;
                else
                    _size = value;

                SizeChanged();
            }
        }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
        }

        private bool _full;

        public bool Full
        {
            get { return _full; }
        }

        private bool _disposed;

        public bool Disposed
        {
            get { return _disposed; }
        }

        #endregion


        #region Constructors

        public BaseShip(ShipPort port)
        {
            if (port == null)
                throw new ArgumentNullException("port");

            _port = port;
            _index = -Width;
            _size = 0;
            _loading = _full = _disposed = false;
        }

        #endregion


        #region Methods

        protected void SizeChanged()
        {
            _full = (_size == Capacity);

            if (_full)
            {
                _port.Loading = null;
                _port.Spawn();

                _loading = false;
            }
        }

        protected bool HasReachedPortIndex()
        {
            return (_index + Width == _port.Index);
        }

        protected void Dispose()
        {
            _disposed = true;
        }

        public void Update()
        {
            if (_loading || _disposed)
                return;

            if (_index >= _port.Size - 1)
                Dispose();
            else
                _index++;

            if (HasReachedPortIndex() && !_full)
            {
                _port.Loading = this;
                _loading = true;
            }
        }

        #endregion
    }
}
