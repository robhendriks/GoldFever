using GoldFever.Core.Content;
using GoldFever.Core.Graphics;
using GoldFever.Core.Level;
using System;
using System.Threading;

namespace GoldFever.Core
{
    public sealed class Game
    {
        private Thread inputThread;
        private Thread logicThread;

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

        public BaseLevel Level
        {
            get { return _levelManager?.Level; }
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public IRenderer Renderer { get; set; }

        public Game(GameOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            _running = false;
            _options = options;
            _score = 0;

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

        private void Loop()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            inputThread = new Thread(HandleInput);
            logicThread = new Thread(HandleLogic);

            inputThread.Start();
            logicThread.Start();
        }

        private void HandleInput()
        {
            while(_running)
            {
                if (Console.KeyAvailable)
                {
                    var info = Console.ReadKey(true);

                    foreach (var actor in _levelManager.Level.Switches)
                        if (actor.Key == info.Key)
                            actor.Toggle();

                    Renderer?.Render();
                }
            }
        }

        private void HandleLogic()
        {
            Renderer?.Render();

            while (_running)
            {
                Renderer?.Render();
                Thread.Sleep(1000);

                try
                {
                    Update();
                }
                catch (GameOverException ex)
                {
                    _running = false;
                    Console.Title = "Game Over!";
                    Console.ReadKey();
                }
            }
        }

        private void Update()
        {
            _levelManager.Level.Update();
        }

        public void Run()
        {
            if (_running)
                throw new InvalidOperationException("Game already running.");

            _running = true;

            try
            {
                Load();
                Loop();
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
