using GoldFever.Core.Generic;
using GoldFever.Core.Level;
using System;

namespace GoldFever.Core.Track
{
    public sealed class SwitchInTrack : SwitchTrack
    {
        private BaseTrack[] targets;

        public override BaseTrack Next
        {
            get
            {
                return targets[SelectedIndex];
            }
        }

        public SwitchInTrack(Vector position, Direction direction, ConsoleKey key)
            : base(position, direction, key)
        {

        }

        public override bool Link(BaseLevel level, out BaseTrack[] results)
        {
            if (_direction != Direction.None)
            {
                results = null;
                return false;
            }

            results = targets = level.GetTracksFacing(_position, 
                Direction.North, 
                Direction.South);

            bool valid = (targets.Length == 2);
            if (!valid)
                throw new LevelLoadException("This type of switch must have exactly two targets.");

            return valid;
        }
    }
}
