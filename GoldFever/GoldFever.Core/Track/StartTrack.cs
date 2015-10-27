using GoldFever.Core.Generic;
using GoldFever.Core.Graphics.Terminal;
using System;

namespace GoldFever.Core.Track
{
    public sealed class StartTrack : BaseTrack
    {
        #region Constructors

        public StartTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        #endregion


        #region Methods

        public override byte Char()
        {
            return 175;
        }

        public override short Attributes()
        {
            return Color.ForegroundRed | Color.BackgroundDarkRed;
        }

        #endregion
    }
}
