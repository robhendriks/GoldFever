using GoldFever.Core.Generic;
using GoldFever.Core.Track;
using System;
using System.Collections.Generic;

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

        public BaseTrack[] GetTracksFacing(Vector position, params Direction[] directions)
        {
            var results = new List<BaseTrack>();

            Vector pos;
            BaseTrack target;

            foreach(var direction in directions)
            {
                if (direction == Direction.None)
                    continue;

                pos = position.Facing(direction);
                target = GetTrackAt(pos);

                if (target == null)
                    continue;

                results.Add(target);
            }

            return results.ToArray();
        }

        private void LinkTracks()
        {
            var visited = new List<BaseTrack>();

            foreach (BaseTrack track in _tracks)
                LinkTrack(track, ref visited);
        }

        private void LinkTrack(BaseTrack current, ref List<BaseTrack> visited)
        {
            if (visited.Contains(current))
                return;

            BaseTrack[] results;
            if(current.Link(this, out results))
            {
                foreach (BaseTrack result in results)
                    LinkTrack(result, ref visited);
            }

            visited.Add(current);
        }

        private void Initialize()
        {
            try
            {
                LinkTracks();
            }
            catch(Exception ex)
            {
                throw new LevelLoadException("Unable to initialize level.", ex);
            }
        }
    }
}
