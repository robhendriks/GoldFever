using GoldFever.Core.Cart;
using GoldFever.Core.Generic;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever.Core.Track
{
    public sealed class EndTrack : BaseTrack
    {
        public EndTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        public override void OnEnter(BaseCart cart)
        {
            cart?.Dispose();
        }

        public override byte Char()
        {
            return 174;
        }

        public override short Attributes()
        {
            return Color.ForegroundRed | Color.BackgroundDarkRed;
        }
    }
}
