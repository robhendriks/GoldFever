using System;
using GoldFever.Core.Content;

namespace GoldFever.Core
{
    public class DefaultGameOptions : GameOptions
    {
        public override string ContentPath
        {
            get { return "GoldFever.Content"; }
        }

        public override IContentSource ContentSource
        {
            get { return new BaseContentSource(); }
        }
    }
}
