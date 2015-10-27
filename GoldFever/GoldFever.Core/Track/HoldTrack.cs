using GoldFever.Core.Generic;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever.Core.Track
{
    public sealed class HoldTrack : BaseTrack
    {
        #region Constructors

        public HoldTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        #endregion


        #region Methods

        public override bool Collides()
        {
            return false;
        }

        public override byte Char()
        {
            return 176;
        }

        public override short Attributes()
        {
            return Color.ForegroundRed | Color.BackgroundDarkRed;
        }

        #endregion
    }
}
