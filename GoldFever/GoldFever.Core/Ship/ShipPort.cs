using GoldFever.Core.Level;
using GoldFever.Core.Model;
using System;
using System.Collections.Generic;

namespace GoldFever.Core.Ship
{
    public sealed class ShipPort
    {
        private BaseLevel _level;

        public BaseLevel Level
        {
            get { return _level; }
        }

        private List<BaseShip> _ships;

        public List<BaseShip> Ships
        {
            get { return _ships; }
        }

        private BaseShip _loading;

        public BaseShip Loading
        {
            get { return _loading; }
            set { _loading = value; }
        }

        private int _size;

        public int Size
        {
            get { return _size; }
        }

        private int _index;

        public int Index
        {
            get { return _index; }
        }

        public ShipPort(BaseLevel level, ShipPortModel data)
        {
            if (level == null)
                throw new ArgumentNullException("level");
            else if(data == null)
                throw new ArgumentNullException("data");

            _level = level;
            _ships = new List<BaseShip>();
            _size = data.Size;
            _index = data.Index;

            if (_index > _size)
                throw new ArgumentOutOfRangeException("Port index cannot exceed size.");

            Spawn();
        }

        public void Update()
        {
            var dispose = new List<BaseShip>();

            foreach (var ship in _ships)
            {
                ship.Update();

                if (ship.Disposed)
                    dispose.Add(ship);
            }

            foreach (var ship in dispose)
                _ships.Remove(ship);
        }

        public void Spawn()
        {
            _ships.Add(new BaseShip(this));
        }

        public void Clear()
        {
            _ships.Clear();
            _loading = null;
        }
    }
}
