using GoldFever.Core.Content;
using GoldFever.Core.Level;
using System;

namespace GoldFever.Core
{
    public sealed class Game
    {
        private bool _running;

        public bool Running
        {
            get { return _running; }
        }

        private GameOptions _options;

        public GameOptions Options
        {
            get { return _options; }
        }

        private ContentManager _contentManager;

        public ContentManager ContentManager
        {
            get { return _contentManager; }
        }

        private LevelManager _levelManager;

        public LevelManager LevelManager
        {
            get { return _levelManager; }
        }

        public Game(GameOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            _running = false;
            _options = options;

            Initialize();
        }

        private void Initialize()
        {
            _contentManager = new ContentManager(_options.ContentPath);
            _levelManager = new LevelManager(this);
        }

        private void Load()
        {
            _levelManager.Load();
        }

        public void Run()
        {
            if (_running)
                throw new InvalidOperationException("Game already running.");

            _running = true;

            try
            {
                Load();
            }
            catch(GameException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new GameException("Unable to load game.", ex);
            }
        }
    }
}
