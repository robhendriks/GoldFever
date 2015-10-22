using GoldFever.Core.Generic;
using GoldFever.Core.Level;
using System;

namespace GoldFever.Core.Track
{
    public sealed class SwitchInTrack : SwitchTrack
    {
        public SwitchInTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        public override bool Link(BaseLevel level, out BaseTrack[] results)
        {
            results = null;
            return false;
        }
    }
}
