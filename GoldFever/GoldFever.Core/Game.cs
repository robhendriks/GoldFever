using GoldFever.Core.Content;
using GoldFever.Core.Graphics;
using GoldFever.Core.Level;
using System;
using System.Threading;

namespace GoldFever.Core
{
    public sealed class Game
    {
        private const int Second = 1000;
        private const int Fps = Second / 15;

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

            Renderer?.Render();
            Loop();
        }

        private void Toggle()
        {
            if (_state == GameState.Playing)
                _state = GameState.Idle;
            else if (_state == GameState.Idle)
                _state = GameState.Playing;
        }

        private int ticks = 5,
                    tick = 0;

        private void Loop()
        {
            while(_state != GameState.GameOver)
            {
                DoInput();

                if (tick == ticks)
                {
                    DoLogic();
                    tick = 0;
                }
                else
                    tick++;

                Thread.Sleep(Fps);
            }
        }

        private void DoInput()
        {
            if (!Console.KeyAvailable)
                return;

            var info = Console.ReadKey(true);

            if (info.Key == ConsoleKey.Escape)
                Toggle();
            else
            if (_state == GameState.Playing)
            {
                foreach (var actor in _levelManager.Level.Switches)
                    if (actor.Key == info.Key)
                        actor.Toggle();
            }

            Renderer?.Render();
        }

        private void DoLogic()
        {
            if (_state != GameState.Playing)
                return;

            Update();
            Renderer?.Render();
        }

        private void Update()
        {
            try
            {
                _levelManager.Level.Update();
            }
            catch (GameOverException ex)
            {
                OnGameOver();
            }
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
