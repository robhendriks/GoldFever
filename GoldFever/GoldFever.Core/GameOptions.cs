using GoldFever.Core.Content;
using System;

namespace GoldFever.Core
{
    public class GameOptions
    {
        #region Properties

        public virtual string ContentPath { get; set; }
        public virtual IContentSource ContentSource { get; set; }

        #endregion
    }
}
