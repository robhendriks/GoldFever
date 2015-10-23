using GoldFever.Core.Generic;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever.Core.Track
{
    public sealed class HoldTrack : BaseTrack
    {
        public HoldTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

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
    }
}
