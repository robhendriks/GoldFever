using GoldFever.Core.Generic;
using System;
using GoldFever.Core.Cart;
using GoldFever.Core.Graphics.Terminal;

namespace GoldFever.Core.Track
{
    public sealed class DropTrack : BaseTrack
    {
        public DropTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        public override void OnEnter(BaseCart cart)
        {
            cart.Empty();
            base.OnEnter(cart);
        }

        public override byte Char()
        {
            return 127;
        }

        public override short Attributes()
        {
            return Color.ForegroundRed | Color.BackgroundDarkRed;
        }
    }
}
