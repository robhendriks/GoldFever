using GoldFever.Core.Cart;
using GoldFever.Core.Generic;
using GoldFever.Core.Model;
using GoldFever.Core.Ship;
using GoldFever.Core.Track;
using System;
using System.Collections.Generic;

namespace GoldFever.Core.Level
{
    public class BaseLevel
    {
        protected LevelManager _manager;

        public LevelManager Manager
        {
            get { return _manager; }
        }
        
        protected LevelMetrics _metrics;

        public LevelMetrics Metrics
        {
            get { return _metrics; }
        }

        protected ShipPort _port;

        public ShipPort Port
        {
            get { return _port; }
        }

        protected BaseTrack[] _tracks;

        public BaseTrack[] Tracks
        {
            get { return _tracks; }
        }

        protected StartTrack[] _depots;

        public StartTrack[] Depots
        {
            get { return _depots; }
        }

        protected SwitchTrack[] _switches;

        public SwitchTrack[] Switches
        {
            get { return _switches; }
        }

        protected List<BaseCart> _carts;

        public List<BaseCart> Carts
        {
            get { return _carts; }
        }

        private Spawner _spawner;

        public Spawner Spawner
        {
            get { return _spawner; }
        }

        public BaseLevel(LevelManager manager, LevelModel data)
        {
            if(manager == null)
                throw new ArgumentNullException("manager");
            else if (data == null)
                throw new ArgumentNullException("data");

            _manager = manager;
            _metrics = LevelMetrics.Zero;
            _port = new ShipPort(this, data.Port);
            _tracks = TrackFactory.GetInstance().Create(data);
            _carts = new List<BaseCart>();
            _spawner = new Spawner(this);

            Initialize();
        }

        protected void IncrementScore()
        {
            if (_port.Loading == null 
             || _port.Loading.Full 
             || !_port.Loading.Loading)
                return;

            _port.Loading.Size++;
            _manager.Game.Score += 10;
        }

        protected void BaseLevel_OnAction(object sender, ActionEventArgs e)
        {
            if (e == null)
                return;

            switch(e.Action)
            {
                default:
                    break;
                case Action.IncrementScore:
                    IncrementScore(); break;
            }
        }

        private void LinkTracks()
        {
            var visited = new List<BaseTrack>();
            var depots = new List<StartTrack>();
            var switches = new List<SwitchTrack>();

            foreach (BaseTrack track in _tracks)
            {
                track.OnAction += BaseLevel_OnAction;

                int x = track.Position.X + 1,
                    y = track.Position.Y + 1;

                if (x > _metrics.Width)
                    _metrics.Width = x;
                if (y > _metrics.Height)
                    _metrics.Height = y;

                if (track is StartTrack)
                    depots.Add((StartTrack)track);
                else if (track is SwitchTrack)
                    switches.Add((SwitchTrack)track);

                LinkTrack(track, ref visited);
            }

            _depots = depots.ToArray();
            _switches = switches.ToArray();

            if (_depots.Length == 0)
                throw new LevelLoadException("Level does not have an entry point.");
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

            foreach (var direction in directions)
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

        //private Random rand = new Random();
        //private int maxSteps = 8,
        //            steps = 0,
        //            maxAmount = 24,
        //            amount = 0;

        public void Update()
        {
            var dispose = new List<BaseCart>();

            #region Debug

            //if (steps >= maxSteps && amount < maxAmount)
            //{
            //    var c1 = new BaseCart();
            //    c1.Current = _depots[0]; //[rand.Next(0, 3)];
            //    _carts.Add(c1);

            //    steps = 0;
            //    amount++;
            //}
            //else
            //    steps++;

            #endregion

            _spawner.Update();
            _port.Update();

            foreach (var cart in _carts)
            {
                if (cart.IsDisposed)
                    dispose.Add(cart);

                cart.Update();
            }

            // Clean up
            foreach (var cart in dispose)
                _carts.Remove(cart);
        }

        public void Clear()
        {
            foreach (var track in _tracks)
                track.Cart = null;

            _carts.Clear();
            _port.Clear();

            _spawner.Reset();
        }
    }
}
