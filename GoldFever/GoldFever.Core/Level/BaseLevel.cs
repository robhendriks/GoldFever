using GoldFever.Core.Generic;
using GoldFever.Core.Track;
using System;

namespace GoldFever.Core.Level
{
    public abstract class BaseLevel
    {
        protected BaseTrack[] _tracks;

        public BaseTrack[] Tracks
        {
            get { return _tracks; }
        }

        public BaseLevel(BaseTrack[] tracks)
        {
            if (tracks == null)
                throw new ArgumentNullException("tracks");
            else if (tracks.Length == 0)
                throw new ArgumentException("Track array is empty.");

            _tracks = tracks;

            Initialize();
        }

        public BaseTrack GetTrackAt(Vector position)
        {
            foreach (var track in _tracks)
                if (track.Position.Equals(position))
                    return track;

            return null;
        }

        protected void Initialize()
        {
            
        }
    }
}
