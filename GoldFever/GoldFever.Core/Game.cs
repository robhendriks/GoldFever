using GoldFever.Core.Content;
using GoldFever.Core.Graphics;
using GoldFever.Core.Level;
using System;
using System.Threading;

namespace GoldFever.Core
{
    public sealed class Game
    {
        #region Private fields

        private Thread inputThread;
        private Thread logicThread;

        #endregion


        #region Properties

        private bool _ready;

        public bool Ready
        {
            get { return _ready; }
        }

        private GameState _state;

        public GameState State
        {
            get { return _state; }
            set
            {
                var tmp = _state;
                _state = value;

                if(_state != tmp)
                    OnGameStateChanged();
            }
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

        #endregion


        #region Constructors

        public Game(GameOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            _ready = false;
            _options = options;

            Initialize();
        }

        #endregion

        private void Initialize()
        {
            _contentManager = new ContentManager(_options.ContentPath);
            _levelManager = new LevelManager(this);
        }

        private void Reset()
        {
            // inputThread?.Abort();
            // logicThread?.Abort();

            _score = 0;
            _levelManager.Level.Clear();
        }

        public void Load()
        {
            if (_ready)
                return;

            try
            {
                _levelManager.Load();
            }
            catch (GameException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new GameException("Unable to load game.", ex);
            }

            _ready = true;
        }

        public void Pause()
        {
            if (_state == GameState.Idle)
                return;

            State = GameState.Idle;
        }

        public void Resume()
        {
            var tmp = _state;

            if (_state == GameState.Playing)
                return;

            State = GameState.Playing;

            // Start actual game
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            inputThread = new Thread(HandleInput);
            logicThread = new Thread(HandleLogic);

            Renderer?.Render();

            // Please dear lord, forgive my sins.
            // Thau shall only start thy threads once.
            if (tmp != GameState.GameOver)
            {
                inputThread.Start();
                logicThread.Start();
            }
        }

        private void Toggle()
        {
            if (_state == GameState.Playing)
                _state = GameState.Idle;
            else if (_state == GameState.Idle)
                _state = GameState.Playing;
        }

        private void HandleInput()
        {
            while(_state != GameState.GameOver)
            {
                if (Console.KeyAvailable)
                {
                    var info = Console.ReadKey(true);

                    if (info.Key == ConsoleKey.Escape)
                        Toggle();
                    else
                    {
                        if (_state != GameState.Idle)
                        {
                            foreach (var actor in _levelManager.Level.Switches)
                                if (actor.Key == info.Key)
                                    actor.Toggle();
                        }
                    }

                    Renderer?.Render();
                }
            }
        }

        private void HandleLogic()
        {
            while (_state != GameState.GameOver)
            {
                if (_state == GameState.Idle)
                    continue;

                Renderer?.Render();
                Thread.Sleep(500);

                try
                {
                    Update();
                }
                catch (GameOverException ex)
                {
                    OnGameOver();
                }
            }
        }

        private void Update()
        {
            _levelManager.Level.Update();
        }

        #region Events

        private void OnGameOver()
        {
            _state = GameState.GameOver;

            Reset();

            if (GameOver != null)
                GameOver(this);
        }

        private void OnGameStateChanged()
        {
            if (GameStateChanged != null)
                GameStateChanged(this);
        }

        public event GameOverHandler GameOver;
        public event GameStateChangedHandler GameStateChanged;

        public delegate void GameOverHandler(object sender);
        public delegate void GameStateChangedHandler(object sender);

        #endregion
    }
}
