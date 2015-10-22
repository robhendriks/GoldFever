using GoldFever.Core.Generic;
using GoldFever.Core.Track;
using System;

namespace GoldFever.Core.Model
{
    public class TrackModel
    {
        public TrackType Type { get; set; }
        public Vector Position { get; set; }
        public Direction Direction { get; set; }
        public ConsoleKey Key { get; set; }
    }
}
