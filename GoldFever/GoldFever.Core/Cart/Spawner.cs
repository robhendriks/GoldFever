using GoldFever.Core.Level;
using System;
namespace GoldFever.Core.Cart
{
    public sealed class Spawner
    {
        #region Constants

        private const int Regular = 6,
                          Fast = 4,
                          Faster = 3,
                          Fastest = 2;

        #endregion


        #region Private Fields

        private BaseLevel level;
        private Random random;

        #endregion


        #region Properties

        private SpawnSpeed _speed;

        public SpawnSpeed Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnSpeedChanged();
            }
        }

        private int _maxSteps;

        public int MaxSteps
        {
            get { return _maxSteps; }
        }

        private int _steps;

        public int Steps
        {
            get { return _steps; }
        }

        #endregion


        #region Constructors

        public Spawner(BaseLevel level)
        {
            if (level == null)
                throw new ArgumentNullException("level");

            this.level = level;
            this.random = new Random();

            Reset();
        }

        #endregion


        #region Methods

        public void Update()
        {
            if (_maxSteps == -1)
                return;

            if (_steps == _maxSteps)
            {
                Spawn();
                _steps = 0;
            }
            else
                _steps++;
        }

        public void Reset()
        {
            _speed = SpawnSpeed.Regular;
            _maxSteps = Regular;
            _steps = 0;
        }

        private void Spawn()
        {
            if (_steps != _maxSteps)
                return;

            var index = random.Next(0, level.Depots.Length);
            var cart = new BaseCart();
            cart.Current = level.Depots[index];

            level.Carts.Add(cart);
        }

        private void OnSpeedChanged()
        {
            _steps = 0;
            _maxSteps = GetMaxSteps();
        }

        private int GetMaxSteps()
        {
            switch(_speed)
            {
                default:
                    return -1;
                case SpawnSpeed.Regular:
                    return Regular;
                case SpawnSpeed.Fast:
                    return Fast;
                case SpawnSpeed.Faster:
                    return Faster;
                case SpawnSpeed.Fastest:
                    return Fastest;
            }
        }

        #endregion
    }
}
