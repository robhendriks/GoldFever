using GoldFever.Core.Cart;
using GoldFever.Core.Generic;
using GoldFever.Core.Level;
using System;

namespace GoldFever.Core.Track
{
    public sealed class SwitchOutTrack : SwitchTrack
    {
        #region Constructors

        public SwitchOutTrack(Vector position, Direction direction, ConsoleKey key)
            : base(position, direction, key)
        {

        }

        #endregion


        #region Methods

        public override bool Link(BaseLevel level, out BaseTrack[] results)
        {
            if (_direction != Direction.None)
            {
                results = null;
                return false;
            }

            results = level.GetTracksFacing(_position, Direction.East);

            bool valid = (results.Length != 0);
            if (valid)
                _next = results[0];

            return valid;
        }

        public override bool CanEnter(BaseCart cart)
        {
            return !Occupied 
                && (Compare(cart.Current.Position) == _mode);
        }

        public override bool Collides()
        {
            return false;
        }

        public override byte Char()
        {
            return (_mode == SwitchMode.Up ? CharDown : CharUp);
        }

        #endregion
    }
}
