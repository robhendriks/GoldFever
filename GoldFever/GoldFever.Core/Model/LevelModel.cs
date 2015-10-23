using System;
using Newtonsoft.Json;

namespace GoldFever.Core.Model
{
    public sealed class LevelModel
    {
        public ShipPortModel Port { get; set; }
        public TrackModel[] Tracks { get; set; }
    }
}
