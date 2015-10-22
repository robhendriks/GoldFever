using GoldFever.Core.Generic;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever.Core.Track
{
    public sealed class StartTrack : BaseTrack
    {
        public StartTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        public override byte Character()
        {
            return 26;
        }

        public override short Attributes()
        {
            return Color.ForegroundWhite | Color.BackgroundDarkYellow;
        }
    }
}
