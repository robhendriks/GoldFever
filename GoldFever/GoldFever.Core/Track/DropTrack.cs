using GoldFever.Core.Generic;
using System;
using GoldFever.Core.Cart;
using GoldFever.Core.Graphics.Terminal;

namespace GoldFever.Core.Track
{
    public sealed class DropTrack : BaseTrack
    {
        #region Constructors

        public DropTrack(Vector position, Direction direction)
            : base(position, direction)
        {

        }

        #endregion


        #region Methods

        public override void OnEnter(BaseCart cart)
        {
            cart.Empty();
            InvokeAction(new ActionEventArgs(Action.IncrementScore));

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

        #endregion
    }
}
