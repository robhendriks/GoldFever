using GoldFever.Core.Cart;
using GoldFever.Core.Generic;
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
    }
}
